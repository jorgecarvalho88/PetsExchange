using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Text.Json;
using TechTalk.SpecFlow.Assist;
using UserApiContract;

namespace UserApiIT.StepDefinitions
{
    [Binding]
    public class GetUserSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public GetUserSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"User endpoint is available")]
        public void GivenUserEndpointIsAvailable()
        {
            //Assert.True(_scenarioContext.ContainsKey("httpClient"));

            var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            // ... Configure test services
            builder.UseTestServer();
        });

            var client = application.CreateClient();

            // Create an HttpClient to send requests to the TestServer
            _scenarioContext.Add("httpClient", client);
        }

        [When(@"I submit a GET request (.*)")]
        public void WhenISubmitAGETRequest(string url)
        {
            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var response = client.GetAsync(url);

            _scenarioContext.Add("responseTask", response);
        }

        [Then(@"I receive a response")]
        public void ThenIReceiveAResponse()
        {
            var task = _scenarioContext.Get<Task<HttpResponseMessage>>("responseTask");
            var response = task.GetAwaiter().GetResult();
            _scenarioContext.Add("response", response);
        }

        [Then(@"the http response status is (.*)")]
        public void ThenTheHttpResponseStatusIs(string status)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.Equal(status, response.StatusCode.ToString());
        }

        [Then(@"the success response content is")]
        public void ThenTheSuccessResponseContentIs(Table table)
        {
            // usar createSet se devolver uma lista de users p/ ex.
            var expectedUser = table.CreateInstance<UserContract>();
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.True(responseUser.IsValid);
            Assert.Equal(expectedUser.UniqueId, responseUser.UniqueId);
        }

        [Then(@"the fail response content is")]
        public void ThenTheFailResponseContentIs(Table table)
        {
            // usar createSet se devolver uma lista de users p/ ex.
            var expectedUser = table.CreateInstance<UserContract>();
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.False(responseUser.IsValid);
            Assert.Equal(expectedUser.Errors.FirstOrDefault(), responseUser.Errors.FirstOrDefault());
        }
    }
}
