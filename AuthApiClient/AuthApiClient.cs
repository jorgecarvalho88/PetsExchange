using AuthApiContract;
using ClientExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApiClient
{
    public class AuthApiClient : ApiClientBase, IAuthApiClient
    {
        public AuthApiClient()
        {
            _uri = new Uri("https://localhost:7262/Auth");
        }

        public async Task<ResponseResult<AuthResultApiContract>> Login(LoginApiContract userLoginDto)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/Login") { Content = Content(userLoginDto) };
            return await HttpRequest<AuthResultApiContract>(requestMessage);
        }

        public async Task<ResponseResult<AuthResultApiContract>> RefreshToken(AuthResultApiContract authResultDto)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/RefreshToken") { Content = Content(authResultDto) };
            return await HttpRequest<AuthResultApiContract>(requestMessage);
        }

        public async Task<ResponseResult<AuthResultApiContract>> Register(RegistrationApiContract userRegistrationDto)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/Register") { Content = Content(userRegistrationDto) };
            return await HttpRequest<AuthResultApiContract>(requestMessage);
        }
    }
}
