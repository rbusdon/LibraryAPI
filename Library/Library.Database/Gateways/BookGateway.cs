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

        public IEnumerable<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBookByISBN(int ISBN) => _context.Books.First
            (book => book.ISBN == ISBN);

        public IEnumerable<Book> CreateBook(List<Book> booksToAdd)
        {
            throw new NotImplementedException();
        }

        public Book UpdateBook(int ISBN)
        {
            var existingBook = _context.Books.SingleOrDefault(b => b.ISBN == ISBN);
            //if (existingBook != null)
            //{
            //    existingBook.Title = book.Title;
            //    existingBook.Author.GivenName = author.FamilyName;
            //    existingBook.Author.FamilyName
            //    existingBook.Author.DateOfBirth
            //    existingBook.Year = author.Year;

            //    _context.SaveChanges();
            //}
            return existingBook;
        }

        public Book DeleteBook(int ISBN)
        {
            var book = _context.Books.SingleOrDefault(b => b.ISBN == ISBN);
            _context.Remove(book);
            _context.SaveChanges();
            return book;
        }
    }
}
