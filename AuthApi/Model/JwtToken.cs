using Microsoft.IdentityModel.Tokens;
using Validations;

namespace AuthApi.Model
{
    public class JwtToken : ValidationBase
    {
        public JwtToken()
        {

        }

        public JwtToken(SecurityToken token, string tokenString, string refreshToken, List<string> errors)
        {
            SetToken(token);
            SetJwt(tokenString);
            SetRefreshToken(refreshToken);

        }

        public string TokenString { get; protected set; }
        public string Token { get; protected set; }
        public string RefreshToken { get; protected set; }

        public void SetJwt(string tokenString)
        {
            TokenString = tokenString;
        }
        public void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public void SetToken(SecurityToken token)
        {
            Token = token.Id;
        }


    }
}
