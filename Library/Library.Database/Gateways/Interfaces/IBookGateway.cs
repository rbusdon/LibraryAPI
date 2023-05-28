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
        List<Book> GetAllBooks(int size, int page);
        Book GetBookByISBN(int ISBN);
        List<Book> CreateBook(List<Book> booksToAdd);
        Book UpdateBook(int ISBN, Book book);
        bool DeleteBook(int ISBN);
    }
}
