using Library.Database.Models;

namespace RMLibrary.API.DTOs
{
    public class BookDTO
    {
        public BookDTO(string title, int authorId, int year, int iSBN)
        {
            Title = title;
            AuthorId = authorId;
            Year = year;
            ISBN = iSBN;
        }

        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public int Year { get; set; }
        public int ISBN { get; set; }
    }
}
