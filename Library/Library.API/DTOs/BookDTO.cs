using Library.Database.Models;

namespace RMLibrary.API.DTOs
{
    public class BookDTO
    {
        public BookDTO(string title, Author? author, int year, int iSBN)
        {
            Title = title;
            Author = author;
            Year = year;
            ISBN = iSBN;
        }
        public string Title { get; set; } = null!;
        public virtual Author? Author { get; set; }
        public int Year { get; set; }
        public int ISBN { get; set; }
    }
}
