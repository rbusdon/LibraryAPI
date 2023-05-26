using Library.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace RMLibrary.Database.Context
{
    public class RMLibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;

        public RMLibraryDbContext(DbContextOptions<RMLibraryDbContext> options) : base(options)
        {
        }
    }
}
