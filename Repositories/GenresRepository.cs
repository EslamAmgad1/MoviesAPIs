namespace MoviesApi.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly ApplicationDbContext _context;
        public GenresRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.OrderBy(g=>g.Name).AsNoTracking().ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }
        public async Task<Genre> AddAsync(Genre genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }
        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }
        public async Task<bool> IsValidGenreAsync(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }


    }
}
