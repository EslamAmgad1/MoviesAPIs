namespace MoviesApi.Repositories
{
    public interface IGenresRepository
    {
        public Task<IEnumerable<Genre>> GetAllAsync();

        public Task<Genre?> GetByIdAsync(byte id);

        public Task<Genre> AddAsync(Genre genre);

        public Genre Update(Genre genre);

        public Genre Delete(Genre genre);

        public Task<bool> IsValidGenreAsync(byte id);

    }
}