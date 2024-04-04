namespace MoviesApi.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;


        public ICollection<Movie> Movies { get; set;} = new List<Movie>();

    }
}
