using Validations;

namespace UserApiContract
{
    public class UserContract : ValidationBase
    {
        public UserContract()
        {
        }

        public UserContract(Guid uniqueId, 
            string firstName, 
            string lastName, 
            string email,
            int? mobileNumber, 
            string address, 
            int postCode, 
            string city, 
            DateTime dateOfBirth,
            string? sitterProfileId,
            string? profilePhotoUrl,
            List<string> errors)
        {
            UniqueId = uniqueId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            MobileNumber = mobileNumber;
            Address = address;
            PostCode = postCode;
            City = city;
            DateOfBirth = dateOfBirth;
            SitterProfileId = sitterProfileId;
            ProfilePhotoUrl = profilePhotoUrl;
            Errors = errors;
        }
        public Guid UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? MobileNumber { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? SitterProfileId { get; set; }
        public string? ProfilePhotoUrl { get; set; }
    }
}