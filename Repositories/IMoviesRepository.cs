namespace MoviesApi.Repositories
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync(byte genreId = 0);
        Task<Movie?> GetByIdAsync(int id);
        Task<Movie> AddAsync(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
    }
}