namespace MoviesApi.DTOS
{
    public class MovieDetailsDto : BaseMovieDto
    {
        public int Id { get; set; }

        public IEnumerable<byte> Poster { get; set; } = new List<byte>();

        public string GenreName { get; set; } = string.Empty;
    }
}
