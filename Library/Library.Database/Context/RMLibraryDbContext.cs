using Library.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
