using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;

namespace AuthApiContract
{
    public class JwtUserContract : ValidationBase
    {
        public JwtUserContract()
        { }

        public JwtUserContract(string tokenId, string jwtToken, Guid uniqueId, string email, List<string> errors)
        {
            TokenId = tokenId;
            JwtToken = jwtToken;
            UniqueId = uniqueId;
            Email = email;
            Errors = errors;
        }

        public string TokenId { get; set; }
        public string JwtToken { get; set; }
        public Guid UniqueId { get; set; }
        public string Email { get; set; }
    }
}
