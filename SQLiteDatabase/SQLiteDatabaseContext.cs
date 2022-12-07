using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDatabaseContext : DbContext
    {
        private IConfiguration _configuration;

        public SQLiteDatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("SqliteConnectionString"));
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
