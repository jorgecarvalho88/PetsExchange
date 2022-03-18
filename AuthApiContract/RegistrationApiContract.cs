using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;

namespace AuthApiContract
{
    public class RegistrationApiContract : ValidationBase
    {
        public RegistrationApiContract()
        { }

        public RegistrationApiContract(string email, string password)
        {
            //SetName(firstName, lastName);
            SetEmail(email);
            SetPassword(password);
        }

        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        //private void SetName(string firstName, string lastName)
        //{
        //    ValidateIsNullOrWhiteSpace(firstName, "firstName");
        //    ValidateIsNullOrWhiteSpace(lastName, "lastName");

        //    FirstName = firstName;
        //    LastName = lastName;
        //}

        private void SetEmail(string email)
        {
            ValidateIsNullOrWhiteSpace(email, nameof(email));
            ValidateEmail(email);

            Email = email;
        }

        private void SetPassword(string password)
        {
            ValidateIsNullOrWhiteSpace(password, nameof(password));

            Password = password;
        }

        private void ValidateIsNullOrWhiteSpace(string value, string property)
        {
            var error = StringValidator.ValidateIsNullOrWhiteSpace(value, property);
            if (error != null)
            {
                this.Errors.Add(error);
            }
        }

        private void ValidateEmail(string email)
        {
            var error = EmailValidatior.IsValidEmail(email);
            if(!string.IsNullOrWhiteSpace(error)) this.Errors.Add(error);
        }
    }
}
