using Microsoft.EntityFrameworkCore;
using MovieCollection.Models;

namespace MovieCollection
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesCollection> MovieCollection { get; set; }
        public DbSet<Director> Directors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}
