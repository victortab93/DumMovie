using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DumMovie.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        [Route("api/preference-options")]
        public async Task<ActionResult<IEnumerable<User>>> GetPreferenceOptions()
        {
            var preferences = await _context.TypePreferences.ToListAsync();
            return Ok(preferences);
        }

        [HttpGet]
        [Route("api/get-users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("api/get-user-by-id")]
        public async Task<ActionResult<User>> GetUser([FromQuery] int id)
        {
            var user = await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new
                {
                    u.UserId,
                    u.Name,
                    UserTypePreferences = u.UserTypePreferences.Select(utp => new
                    {
                        utp.Id,
                        utp.IdTypePreferenceNavigation.Name
                        // Include other desired properties
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
        [Route("api/create-user")]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserReq user)
        {
            User userNew = new();
            userNew.Name = user.Name;

            _context.Users.Add(userNew);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = userNew.UserId }, user);
        }

        [HttpPost]
        [Route("api/add-preferences-to-user")]
        public async Task<ActionResult<User>> AddPreferenceToUser([FromBody] AddPreferencesToUserReq req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.UserId == req.userId);

            if (user == null)
                return NotFound();

            user.UserTypePreferences.Add(new UserTypePreference { IdTypePreference = req.preferenceId });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) { }


            return Ok();
        }

        [HttpDelete]
        [Route("api/delete-user-preference-by-id")]
        public async Task<IActionResult> DeleteUserPreference([FromQuery] int id)
        {
            if (id <=0)
            {
                return BadRequest();
            }


            try
            {
                var userTypePreference = await _context.UserTypePreferences.FindAsync(id);
                if (userTypePreference != null)
                {
                    _context.UserTypePreferences.Remove(userTypePreference);
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