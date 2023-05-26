using Library.Database.Models;
using Microsoft.EntityFrameworkCore;
using RMLibrary.Database.Context;
using RMLibrary.Database.Gateways.Interfaces;

namespace RMLibrary.Database.Gateways
{
    public class AuthorGateway : IAuthorGateway
    {
        private readonly RMLibraryDbContext _context;
        public AuthorGateway(RMLibraryDbContext context) => _context = context;
        public IEnumerable<Author> GetAllAuthors() => _context.Authors.ToList()
            .Where(author => author.Books.Any());
        public Author GetAuthorById(int id) => _context.Authors.First
            (author => author.Id == id);

        public Author CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }

        public Author UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
            return author;
        }

        public bool DeleteAuthor(int id)
        {
            _context.Remove(_context.Authors.First(a => a.Id == id));
            _context.SaveChanges();
            return true;
        }
    }
}