using Library.Database.Models;
using Microsoft.EntityFrameworkCore;
using RMLibrary.Database.Context;
using RMLibrary.Database.Gateways.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLibrary.Database.Gateways
{
    public class AuthorGateway : IAuthorGateway
    {
        private readonly RMLibraryDbContext _context;
        public AuthorGateway(RMLibraryDbContext context) => _context = context;
        public IEnumerable<Author> GetAllAuthors() => _context.Authors
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
            var existingAuthor = _context.Authors.Find(author.Id);
            if (existingAuthor != null)
            {
                existingAuthor.GivenName = author.GivenName;
                existingAuthor.FamilyName = author.FamilyName;
                existingAuthor.DateOfBirth = author.DateOfBirth;

                _context.SaveChanges();
            }
            return existingAuthor;
        }

        public Author DeleteAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);
            _context.Remove(author);
            _context.SaveChanges();
            return author;
        }
    }
}