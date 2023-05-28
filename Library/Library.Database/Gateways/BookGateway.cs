using Library.Database.Models;
using Microsoft.EntityFrameworkCore;
using RMLibrary.Database.Context;
using RMLibrary.Database.Gateways.Interfaces;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RMLibrary.Database.Gateways
{
    public class BookGateway : IBookGateway
    {
        private readonly RMLibraryDbContext _context;
        public BookGateway(RMLibraryDbContext context) => _context = context;

        public List<Book> GetAllBooks(int size, int page) => new List<Book>();
        //_context.Books.Include(a => a.Author).Skip(size * page).Take(size).ToList();

        public Book GetBookByISBN(int ISBN) => _context.Books.FirstOrDefault
            (book => book.ISBN == ISBN);

        public List<Book> CreateBook(List<Book> books)
        {
            List<Book> newShelf = new List<Book>();
            foreach (var book in books)
            {
                var associatedAuthor = _context.Books.FirstOrDefault(a => a.AuthorId == book.AuthorId);
                if (associatedAuthor is not null)
                {
                    newShelf.Add(new Book
                    {
                        Id = null,
                        Title = book.Title,
                        Author = _context.Authors.First(b => b.Id == book.AuthorId),
                        Year = book.Year,
                        ISBN = book.ISBN
                    });
                }
                if (_context.Books.Any(p => p.ISBN == book.ISBN))
                {
                    throw new NotImplementedException();
                }
            }
            foreach(Book book in newShelf)
            {
                _context.Books.Add(book);
            }
            _context.SaveChanges();
            return newShelf;
        }

        public Book UpdateBook(int ISBN, Book book)
        {
            if (_context.Books.SingleOrDefault(b => b.ISBN == ISBN) is null)
            {
                throw new NotImplementedException();
            }
            book.Author = _context.Authors.SingleOrDefault(a => a.Id == book.AuthorId);
            _context.Books.Update(book);
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return book;
        }

        public bool DeleteBook(int ISBN)
        {
            _context.Books.Remove(GetBookByISBN(ISBN));
            _context.SaveChanges();
            return true;
        }
    }
}
