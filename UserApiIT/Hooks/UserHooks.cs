using System.Text.Json;
using TechTalk.SpecFlow;
using UserApiContract;

namespace UserApiIT.Hooks
{
    [Binding]
    public sealed class UserHooks
    {
        private ScenarioContext _scenarioContext;

        public UserHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeStep()]
        public void BeforeScenarioWithTag()
        {
            //var client = _scenarioContext.Get<HttpClient>("httpClient");
            //var task = client.DeleteAsync($"/User?uniqueId={createdUser.UniqueId}");
            //var response = await task;
            //var responseContent = response.Content.ReadAsStringAsync().Result;
            //var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //Assert.True(responseUser?.IsValid);
        }

        [AfterScenario("add")]
        public async Task AfterScenarioWithTag()
        {
            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var createdUser = _scenarioContext.Get<UserContract>("responseUser");
            var task = client.DeleteAsync($"/User?uniqueId={createdUser.UniqueId}");
            var response = await task;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.True(responseUser?.IsValid);
        }
    }
}