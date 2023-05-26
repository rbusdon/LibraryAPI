namespace Library.Database.Models
{
    public record Book(int? Id = default, string Title = "", int Year = default, int ISBN = default)
    {
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
