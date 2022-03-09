using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Validations
{
    public class PasswordValidator
    {
        public static string GeneratePassword(string password)
        {
            var passwordHash = BC.HashPassword(password);
            return passwordHash;
        }

        public static bool ValidatePassword(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
