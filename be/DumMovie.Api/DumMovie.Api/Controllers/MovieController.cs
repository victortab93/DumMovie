using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DumMovie.Api.Controllers
{
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/get-movies")]
        public async Task<ActionResult<IEnumerable<User>>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }
        [HttpGet]
        [Route("api/get-suggestions-by-user")]
        public async Task<ActionResult<IEnumerable<User>>> GetSuggestionByUser(int idUser)
        {
            var preferences = await _context.UserTypePreferences
                .Where(w => w.IdUser == idUser)
                .Select(s => s.IdTypePreference)
                .ToListAsync();

            preferences = preferences ?? new List<int> ();

            var moviesSuggestion = await _context.MovieTypePreferences
                .Where(w => preferences.Contains(w.IdTypePreference))
                .Select(s => 
                new {
                    id = s.IdMovie,
                    title = s.IdMovieNavigation.Title,
                    types = string.Join(',', s.IdMovieNavigation.MovieTypePreferences.Select(s => s.IdTypePreferenceNavigation.Name).ToList())
                })
                .ToListAsync();   

            return Ok(moviesSuggestion);
        }

        [HttpGet]
        [Route("api/get-movie-by-id")]
        public async Task<ActionResult<User>> GetMovie([FromQuery] int id)
        {
            var user = await _context.Movies
                .Where(u => u.MovieId == id)
                .Select(u => new
                {
                    u.MovieId,
                    u.Title,
                    MovieTypePreferences = u.MovieTypePreferences.Select(utp => new
                    {
                        utp.Id,
                        utp.IdTypePreferenceNavigation.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        [Route("api/create-movie")]
        public async Task<ActionResult<User>> CreateMovie([FromBody] CreateMovieReq user)
        {
            Movie movieNew = new();
            movieNew.Title = user.Title;

            _context.Movies.Add(movieNew);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movieNew.MovieId }, user);
        }

        [HttpPost]
        [Route("api/add-preferences-to-movie")]
        public async Task<ActionResult<User>> AddPreferenceToMovie([FromBody] AddPreferencesToMovieReq req)
        {
            var user = await _context.Movies
                .FirstOrDefaultAsync(f => f.MovieId == req.movieId);

            if (user == null)
                return NotFound();

            user.MovieTypePreferences.Add(new MovieTypePreference { IdTypePreference = req.preferenceId });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) { }


            return Ok();
        }

        [HttpDelete]
        [Route("api/delete-movie-preference-by-id")]
        public async Task<IActionResult> DeleteMoviePreference([FromQuery] int id)
        {
            if (id <=0)
            {
                return BadRequest();
            }


            try
            {
                var movieTypePreference = await _context.MovieTypePreferences.FindAsync(id);
                if (movieTypePreference != null)
                {
                    _context.MovieTypePreferences.Remove(movieTypePreference);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
            }

            return NoContent();
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }
    }
}