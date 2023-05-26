using Library.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLibrary.Database.Gateways.Interfaces
{
    public interface IBookGateway
    {
        IEnumerable<Book> GetAllAuthors();
        Book GetBookByISBN(int ISBN);
        IEnumerable<Book> CreateBook(List<Book> booksToAdd);
        Book UpdateBook(int ISBN);
        Book DeleteBook(int ISBN);
    }
}
