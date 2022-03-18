using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;

namespace AuthApiContract
{
    public class LoginApiContract : ValidationBase
    {
        public LoginApiContract()
        { }

        public LoginApiContract(string email, string password)
        {
            SetEmail(email);
            Password = password;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        private void SetEmail(string email)
        {
            ValidateEmail(email);
            Email = email;
        }

        private void ValidateEmail(string email)
        {
            var error = EmailValidatior.IsValidEmail(email);
            if (!string.IsNullOrWhiteSpace(error)) this.Errors.Add(error);
        }
    }
}
