namespace MoviesApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions options ) : base( options ) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }



    }
}
