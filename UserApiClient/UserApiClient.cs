using ClientExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApiContract;

namespace UserApiClient
{
    public class UserApiClient : ApiClientBase, IUserApiClient
    {
        public UserApiClient()
        {
            _uri = new Uri("https://localhost:7197/User");
        }

        public async Task<ResponseResult<UserContract>> GetUser(Guid userId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _uri + "/" + userId.ToString());
            return await HttpRequest<UserContract>(requestMessage);
        }

        public async Task<ResponseResult<UserContract>> UpdateUser(UserContract User)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, _uri) { Content = Content(User) };
            return await HttpRequest<UserContract>(requestMessage);
        }

        public async Task<ResponseResult<UserContract>> CreateUser(UserContract User)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _uri) { Content = Content(User) };
            return await HttpRequest<UserContract>(requestMessage);
        }

        public async Task<ResponseResult<UserContract>> DeleteUser(Guid userId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, _uri + "?uniqueId=" + userId.ToString());
            return await HttpRequest<UserContract>(requestMessage);
        }
    }
}
