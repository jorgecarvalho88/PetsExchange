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

        public User(string firstName, string lastName, string email, string password)
        {
            SetName(firstName, lastName);
            SetEmail(email);
            SetPasswordHash(password);
        }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }
        public bool ConfirmedEmail { get; protected set; }

        public void SetName(string firstName, string lastName)
        {
            ValidateIsNullOrWhiteSpace(firstName, "firstName");
            ValidateIsNullOrWhiteSpace(lastName, "lastName");

            ValidateLength(firstName, "firstName", 50);
            ValidateLength(lastName, "lastName", 50);

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

        public void SetPasswordHash(string password)
        {
            ValidateIsNullOrWhiteSpace(password, "password");
            var passwordHash = Validations.PasswordValidator.GeneratePassword(password);
            if(string.IsNullOrWhiteSpace(passwordHash))
            {
                this.Errors.Add("Error, try a different password");
            }

            PasswordHash = passwordHash;
        }

        public void SetConfirmedEmail(bool isConfirmed)
        {
            ConfirmedEmail = isConfirmed;
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
    }
}
