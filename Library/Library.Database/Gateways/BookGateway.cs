﻿using Library.Database.Models;
using Microsoft.EntityFrameworkCore;
using RMLibrary.Database.Context;
using RMLibrary.Database.Gateways.Interfaces;

namespace RMLibrary.Database.Gateways
{
    public class BookGateway : IBookGateway
    {
        private readonly RMLibraryDbContext _context;
        public BookGateway(RMLibraryDbContext context) => _context = context;

        public List<Book> GetAllBooks(int size, int page) => _context.Books.Include(a => a.Author).Skip(size * page).Take(size).ToList();

        public Book GetBookByISBN(int ISBN) => _context.Books.FirstOrDefault
            (book => book.ISBN == ISBN);

        public List<Book> CreateBook(List<Book> books)
        {
            List<Book> newShelf = new List<Book>();
            foreach (var book in books)
            {
                if (_context.Authors.Any(p => p.Id == book.AuthorId))
                {
                    if (_context.Books.Any(p => p.ISBN == book.ISBN))
                    {
                        throw new NotImplementedException();
                    }
                    var newBook = new Book
                    {
                        Title = book.Title,
                        AuthorId = book.AuthorId,
                        Author = _context.Authors.First(p => p.Id == book.AuthorId),
                        Year = book.Year,
                        ISBN = book.ISBN
                    };
                    newShelf.Add (newBook);
                }
            }
            _context.Books.AddRange(newShelf);
            _context.SaveChanges();
            newShelf = _context.Books.Include(p => p.Author).AsEnumerable().Where(p => books.Any(g => g.ISBN == p.ISBN)).ToList();
            return newShelf;
        }

        public Book UpdateBook(int ISBN, Book book)
        {
            var existingBook = _context.Books.SingleOrDefault(b => b.ISBN == ISBN);
            if (existingBook is null)
            {
                throw new NotImplementedException();
            }

            _context.Entry(existingBook).State = EntityState.Detached;
            book.Author = _context.Authors.SingleOrDefault(a => a.Id == book.AuthorId);
            _context.Books.Update(book);
            _context.SaveChanges();

            var updatedBook = _context.Books.Include(p => p.Author)
                .First(p => p.ISBN == book.ISBN);

            return updatedBook;
        }


        public bool DeleteBook(int ISBN)
        {
            _context.Books.Remove(GetBookByISBN(ISBN));
            _context.SaveChanges();
            return true;
        }
    }
}
