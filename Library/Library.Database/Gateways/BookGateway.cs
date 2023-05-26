using Library.Database.Models;
using Microsoft.EntityFrameworkCore;
using RMLibrary.Database.Context;
using RMLibrary.Database.Gateways.Interfaces;

namespace RMLibrary.Database.Gateways
{
    public class BookGateway : IBookGateway
    {
        private readonly RMLibraryDbContext _context;
        public BookGateway(RMLibraryDbContext context) => _context = context;

        public IEnumerable<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBookByISBN(int ISBN) => _context.Books.First
            (book => book.ISBN == ISBN);

        public List<Book> CreateBook(List<Book> newShelf)
        {
            foreach (var book in newShelf)
            {
                if (_context.Books.Any(p => p.ISBN == book.ISBN))
                {
                    throw new NotImplementedException($"Book with ISBN {book.ISBN} has been found in archive. Retry");
                }
            }
            _context.Books.AddRange(newShelf);
            _context.SaveChanges();
            return newShelf;
        }

        public Book UpdateBook(int ISBN, Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.ISBN == ISBN);
            if (existingBook != null)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            return book;
        }

        public bool DeleteBook(int ISBN)
        {
            _context.Remove(_context.Books.First(b => b.ISBN == ISBN));
            _context.SaveChanges();
            return true;
        }
    }
}
