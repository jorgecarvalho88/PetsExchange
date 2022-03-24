using AuthApi.Infrastructure.RefreshToken;
using AuthApi.Model;
using AuthApiContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Service.RefreshToken
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly UserManager<IdentityUser> _userManager;

        public RefreshTokenService(IOptionsMonitor<JwtConfig> optionsMonitor,
            IRefreshTokenRepository refreshTokenRepository,
            TokenValidationParameters tokenValidationParams,
            UserManager<IdentityUser> userManager)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParams = tokenValidationParams;
            _userManager = userManager;
        }

        public AuthResultApiContract GenerateJwtToken(IdentityUser user)
        {
            // é o que vai ser responsavel por criar o Token
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // a chave de segurança (Secret) no appsettings
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    // para o IdentityFramework saber qual é o user que tem o login feito
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
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

            var refreshToken = new Model.RefreshToken(user.Id, token.Id);

            Add(refreshToken);

            return new AuthResultApiContract()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
            };
        }

        public AuthResultApiContract VerifyRefreshToken(AuthResultApiContract authToken)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                // 1ª VALIDAÇÃO
                // valida se a o token, é um token válido para a nossa aplicação
                var tokenInVerification = jwtTokenHandler.ValidateToken(authToken.Token, _tokenValidationParams, out var validatedToken);

                // 2ª VALIDAÇÃO
                // se é deste tipo de objecto e se foi inicializado vai validar a encriptação
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    // Verifica se o token foi gerado pelao mesmo método de encriptação
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        authToken.Errors.Add("Token validation failed");
                        return authToken;
                    }
                }

                // 3ª VALIDAÇÃO
                // VER SE ESTÁ DENTRO DO TEMPO DE EXPIRAÇÃO
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                // convert utcExpireDate to actual date
                var expDate = UnixTimeStampToDateTime(utcExpireDate);

                if (expDate > DateTime.UtcNow)
                {
                    authToken.Errors.Add("Token has not yet expired");
                    return authToken;
                }

                // 4ª VALIDAÇÃO
                // VER SE O TOKEN EXISTE NA BASE DE DADOS RefreshToken

                var storedToken = Get(authToken.RefreshToken);

                // Se não exisit....
                if (storedToken is null)
                {
                    authToken.Errors.Add("Token does not exist");
                    return authToken;
                }

                // 5ª VALIDAÇÃO
                // verificar se ja foi usado o token
                if (storedToken.IsUsed)
                {
                    authToken.Errors.Add("Token has been used");
                    return authToken;
                }

                // 6ª VALIDAÇÃO
                // verificar se o token já foi revogado
                if (storedToken.IsRevoked)
                {
                    authToken.Errors.Add("Token has been revoked");
                    return authToken;
                }

                // 7ª VALIDAÇÃO
                // verificar se o JTI do token corresponde ao ID do refresh token armazenado na DB
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    authToken.Errors.Add("Token does not match");
                    return authToken;
                }

                // update current token to be revoked before updating to a new one
                storedToken.SetIsUsed();
                _refreshTokenRepository.Update(storedToken);

                // get user logged in e criar novo token com refresh token
                var dbUser = _userManager.FindByIdAsync(storedToken.UserId).GetAwaiter().GetResult();
                return GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                authToken.Errors.Add(ex.Message);
                return authToken;
            }

        }

        private Model.RefreshToken Get(string refreshToken)
        {
            return _refreshTokenRepository.GetByRefreshToken(refreshToken);
        }

        private Model.RefreshToken Add(Model.RefreshToken refreshToken)
        {
            _refreshTokenRepository.BeginTransaction();

            var newRefreshToken = new Model.RefreshToken(refreshToken.UserId, refreshToken.JwtId);

            //validate data is valid
            if (newRefreshToken.IsValid)
            {
                _refreshTokenRepository.Create(newRefreshToken);
                _refreshTokenRepository.Commit();
                _refreshTokenRepository.CommitTransaction();
            }
            else
            {
                _refreshTokenRepository.RollBackTransaction();
                refreshToken.Errors.AddRange(newRefreshToken.Errors);
            }

            return refreshToken;
        }

        #region Methods
        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTimeVal;
        }
        #endregion
    }
}
