namespace MoviesApi.DTOS
{
    public class UpdateMovieDto : BaseMovieDto
    {
        public IFormFile? Poster { get; set; }

    }
}
