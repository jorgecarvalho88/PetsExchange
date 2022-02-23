using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace ClientExtension
{
    public abstract class ApiClientBase
    {
        protected HttpClient _client;
        protected Uri _uri;

        public ApiClientBase()
        {
            CreateClient();
        }

        private void CreateClient()
        {
            _client = new HttpClient();
        }

        protected StringContent Content(object contentObject)
        {
            //return new StringContent(JsonSerializer.Serialize(contentObject), Encoding.UTF8, "application/json");

            return new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
        }

        protected async Task<ResponseResult<T>> HttpRequest<T>(HttpRequestMessage requestMessage)
        {
            using (var httpResponse = await _client.SendAsync(requestMessage, default(CancellationToken)))
            {
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                CreateClient();

                //return new ResponseResult<T>(JsonSerializer.Deserialize<T>(httpContent), httpResponse);

                return new ResponseResult<T>(JsonConvert.DeserializeObject<T>(httpContent), httpResponse);
            }
        }
    }
}