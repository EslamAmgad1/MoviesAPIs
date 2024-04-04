namespace MoviesApi.DTOS
{
    public class AddMovieDto : BaseMovieDto
    {
        public IFormFile Poster { get; set; } = default!;
    }
}
