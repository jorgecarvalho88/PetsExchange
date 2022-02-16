using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public static class EmailValidatior
    {
        public static string? Validate(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
            }
            catch (Exception)
            {
                return "email: The email is invalid";
            }

            return null;
        }
    }
}
