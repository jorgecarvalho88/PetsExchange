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

        public User(string name, string email)
        {
            SetName(name);
            SetEmail(email);
        }

        public string Name { get; protected set; }
        public string Email { get; protected set; }

        public void SetName(string name)
        {
            ValidateIsNullOrWhiteSpace(name, "name");
            ValidateLength(name, "name", 50);
            Name = name;
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
