using Library.Database.Models;

namespace RMLibrary.Database.Gateways.Interfaces
{
    public interface IAuthorGateway
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorById (int id);
        Author CreateAuthor (Author author);
        Author UpdateAuthor (Author author);
        bool DeleteAuthor (int id);
    }
}
