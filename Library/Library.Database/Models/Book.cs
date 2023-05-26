namespace Library.Database.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public virtual Author? Author { get; set; }
        public int Year { get; set; }
        public int ISBN { get; set; }
    }
}
