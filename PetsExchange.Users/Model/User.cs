using DataExtension;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Validations;

namespace UserApi.Model
{
    public class User : BaseEntity
    {
        public User()
        { }

        public User(string firstName, 
            string lastName, 
            string email, 
            int? mobileNumber, 
            string address, 
            int postCode, 
            string city, 
            DateTime dateOfBirth,
            string? sitterProfileId,
            string? profilePhotoUrl)
        {
            SetName(firstName, lastName);
            SetEmail(email);
            SetMobileNumer(mobileNumber);
            SetAddress(address);
            SetPostCode(postCode);
            SetCity(city);
            SetDateOfBirth(dateOfBirth);
            SetSitterProfileId(SitterProfileId);
            SetProfilePhotoUrl(ProfilePhotoUrl);
        }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public int? MobileNumber { get; protected set; }
        public string Address { get; protected set; }
        public int PostCode { get; protected set; }
        public string City { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public string? SitterProfileId { get; protected set; }
        public string? ProfilePhotoUrl { get; protected set; }

        public void SetName(string firstName, string lastName)
        {
            ValidateIsNullOrWhiteSpace(firstName, "firstName");
            ValidateIsNullOrWhiteSpace(lastName, "lastName");

            ValidateLength(firstName, "firstName", 20);
            ValidateLength(lastName, "lastName", 20);

            FirstName = firstName;
            LastName = lastName;
        }

        public void SetEmail(string email)
        {
            var error = EmailValidatior.IsValidEmail(email);
            if (error != null)
            {
                this.Errors.Add(error);
            }

            ValidateLength(email, "email", 50);

            Email = email;
        }

        public void SetMobileNumer(int? mobileNumber)
        {
            MobileNumber = mobileNumber;
        }

        public void SetAddress(string address)
        {
            ValidateIsNullOrWhiteSpace(address, "address");
            ValidateLength(address, "address", 50);
            Address = address;
        }

        public void SetPostCode(int postCode)
        {
            PostCode = postCode;
        }

        public void SetCity(string city)
        {
            ValidateIsNullOrWhiteSpace(city, "city");
            ValidateLength(city, "city", 20);
            City = city;
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            ValidateIsNullOrWhiteSpace(dateOfBirth.ToShortDateString(), "date of birth");
            ValidateIsOfAge(dateOfBirth);
            DateOfBirth = dateOfBirth;
        }

        public void SetSitterProfileId(string? sitterId)
        {
            SitterProfileId = sitterId;
        }

        public void SetProfilePhotoUrl(string? photoUrl)
        {
            ProfilePhotoUrl = photoUrl;
        }

        private void ValidateLength(string value, string property, int length)
        {
            var error = StringValidator.ValidateLength(value, property, length);
            if (error != null)
            {
                this.Errors.Add(error);
            }
        }

        private void ValidateIsNullOrWhiteSpace(string value, string property)
        {
            var error = StringValidator.ValidateIsNullOrWhiteSpace(value, property);
            if (error != null)
            {
                this.Errors.Add(error);
            }
        }

        private void ValidateIsOfAge(DateTime dateOfBirth)
        {
            var error = DateValidator.ValidateIsOfAge(dateOfBirth);
            if (error != null)
            {
                this.Errors.Add(error);
            }
        }
    }
}
