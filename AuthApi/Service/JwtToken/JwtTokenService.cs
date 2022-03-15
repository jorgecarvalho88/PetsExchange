using AuthApi.Model;
using AuthApiContract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Service.JwtToken
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtConfig _jwtConfig;
        public JwtTokenService(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        public JwtUserContract GenerateJwtToken(JwtUserContract user)
        {
            // é o que vai ser responsavel por criar o Token
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // a chave de segurança (Secret) no appsettings
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.UniqueId.ToString()),
                    // para o IdentityFramework saber qual é o user que tem o login feito
                    new Claim(ClaimTypes.NameIdentifier, user.UniqueId.ToString()),
                    //new Claim(ClaimTypes.Role, "Admin"),
                    //new Claim(ClaimTypes.Role, "User"),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    // Sub referes to a unique id
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    // para user o id criado no token refresh para quando passar o expire date
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature) // algoritmo usado para gerar o token
            };

            // gera o token de segurança
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            // vai converter o token the segurança para um token string que vai ser enviado para o user
            var jwtToken = jwtTokenHandler.WriteToken(token);

            if (jwtToken == null)
            {
                user.Errors.Add("Error generating JwtToken");
            }
            else
            {
                user.JwtToken = jwtToken;
                user.TokenId = token.Id;
            }
            
            return user;
        }
    }
}
