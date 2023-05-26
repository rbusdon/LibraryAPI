using Library.Database.Models;
using RMLibrary.Database.Context;
using RMLibrary.Database.Gateways.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLibrary.Database.Gateways
{
    public class BookGateway : IBookGateway
    {
        private readonly RMLibraryDbContext _context;
        public BookGateway(RMLibraryDbContext context) => _context = context;
        public IEnumerable<Book> CreateBook(List<Book> booksToAdd)
        {
            throw new NotImplementedException();
        }

        public Book DeleteBook(int ISBN)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Book GetBookByISBN(int ISBN)
        {
            throw new NotImplementedException();
        }

        public Book UpdateBook(int ISBN)
        {
            throw new NotImplementedException();
        }
    }
}
