namespace Library.Database.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string GivenName { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
