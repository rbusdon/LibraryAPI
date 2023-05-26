namespace Library.Database.Models
{
    public record Author(int? Id = default, string GivenName = "", string FamilyName = "", DateTime DateOfBirth = default)
    {
        public IEnumerable<Book> Books = new HashSet<Book>();
        public string FullName => $"{GivenName} {FamilyName}";
    }
}