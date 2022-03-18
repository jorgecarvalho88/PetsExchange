using AuthApi.Service.RefreshToken;
using AuthApiContract;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRefreshTokenService _refreshTokenService;
        public AuthService(UserManager<IdentityUser> userManager,
            IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _refreshTokenService = refreshTokenService;
        }
        public AuthResultApiContract Register(RegistrationApiContract user)
        {
            var authResult = new AuthResultApiContract();

            if (user.IsValid)
            {
                var existingUser = _userManager.FindByEmailAsync(user.Email).GetAwaiter().GetResult();

                if (existingUser is not null)
                {
                    authResult.Errors.Add("Email already in use");
                    return authResult;
                }

                var newUser = new IdentityUser()
                {
                    Email = user.Email,
                    UserName = user.Email,
                    EmailConfirmed = true //
                };

                var isCreated = _userManager.CreateAsync(newUser, user.Password).GetAwaiter().GetResult();

                if (isCreated.Succeeded)
                {
                    return _refreshTokenService.GenerateJwtToken(newUser);
                }
                else
                {
                    authResult.Errors.AddRange(isCreated.Errors.Select(x => x.Description));
                    return authResult;
                }
            }

            authResult.Errors.AddRange(user.Errors);
            return authResult;
        }

        public AuthResultApiContract Login(LoginApiContract userLogin)
        {
            var authResult = new AuthResultApiContract();

            if(userLogin.IsValid)
            {
                var existingUser = _userManager.FindByEmailAsync(userLogin.Email).GetAwaiter().GetResult();

                if (existingUser is null)
                {
                    authResult.Errors.Add("Invalid Login Request");
                    return authResult;
                }                   

                // Verifica se a password está correta
                var isCorrect = _userManager.CheckPasswordAsync(existingUser, userLogin.Password).GetAwaiter().GetResult();

                if (!isCorrect)
                {
                    authResult.Errors.Add("Invalid Login Request");
                    return authResult;
                }

                authResult = _refreshTokenService.GenerateJwtToken(existingUser);

                return authResult;
            }

            authResult.Errors.AddRange(userLogin.Errors);
            return authResult;
        }

        public AuthResultApiContract RefreshToken(AuthResultApiContract authResult)
        {
            return _refreshTokenService.VerifyRefreshToken(authResult);
        }
    }
}
