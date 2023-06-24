namespace DumMovie.Api
{
    public class CreateUserReq
    {
        public string Name { get; set; }
    }
    public class CreateMovieReq
    {
        public string Title { get; set; }
    }
    public class AddPreferencesToUserReq
    {
        public int userId { get; set; }
        public int preferenceId { get; set; }
    }
    public class AddPreferencesToMovieReq
    {
        public int movieId { get; set; }
        public int preferenceId { get; set; }
    }
}
