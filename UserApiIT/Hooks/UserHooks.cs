using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using UserApiContract;

namespace UserApiIT.Hooks
{
    [Binding]
    public sealed class UserHooks
    {
        private ScenarioContext _scenarioContext;
        private UserContract testUser = new UserContract()
        {
            FirstName = "test",
            LastName = "user",
            Email = "user.delete@test.com",
            MobileNumber = 935469874,
            Address = "Rua do lado",
            PostCode = 4710010,
            City = "Braga",
            DateOfBirth = DateTime.Now.AddYears(-20),
        };

        public UserHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //[BeforeScenario("delete")]
        //public void AddHttpClient()
        //{
        //    var application = new WebApplicationFactory<Program>()
        //.WithWebHostBuilder(builder =>
        //{
        //    // ... Configure test services
        //    builder.UseTestServer();
        //});

        //    var client = application.CreateClient();

        //    // Create an HttpClient to send requests to the TestServer
        //    _scenarioContext.Add("httpDeleteClient", client);
        //}

        //[BeforeScenario("delete")]
        //public void BeforeScenarioWithTag()
        //{
        //    var content = JsonSerializer.Serialize<UserContract>(testUser);
        //    var data = new StringContent(content, Encoding.UTF8, "application/json");

        //    var client = _scenarioContext.Get<HttpClient>("httpDeleteClient");
        //    var task = client.PostAsync("/User", data);

        //    var response = task.GetAwaiter().GetResult();

        //    var responseContent = response.Content.ReadAsStringAsync().Result;
        //    var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    Assert.True(responseUser?.IsValid);

        //    _scenarioContext.Add("testUser", responseUser);
        //}

        [AfterScenario("add")]
        public void AfterScenarioWithTag()
        {
            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var createdUser = _scenarioContext.Get<UserContract>("responseUser");
            var task = client.DeleteAsync($"/User?uniqueId={createdUser.UniqueId}");
            var response = task.GetAwaiter().GetResult();
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.True(responseUser?.IsValid);
        }

        //[AfterScenario("update")]
        //public async Task AfterScenarioUpdate()
        //{

        //}
    }
}