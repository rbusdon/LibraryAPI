namespace RMLibrary.API.DTOs
{
    public class AuthorDTO
    {
        public AuthorDTO(string givenName, string familyName, DateTime dateOfBirth)
        {
            GivenName = givenName;
            FamilyName = familyName;
            DateOfBirth = dateOfBirth;
        }
        public string GivenName { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
