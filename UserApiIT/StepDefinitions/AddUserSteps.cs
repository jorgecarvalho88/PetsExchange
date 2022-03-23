using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserApiContract;

namespace UserApiIT.StepDefinitions
{
    [Binding]
    public class AddUserSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public AddUserSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I submit a POST request (.*)")]
        public void WhenISubmitAPOSTRequestUser(string url, Table table)
        {
            var newUser = table.CreateInstance<UserContract>();
            var content = JsonSerializer.Serialize<UserContract>(newUser);
            var data = new StringContent(content, Encoding.UTF8, "application/json");

            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var response = client.PostAsync(url, data);

            _scenarioContext.Add("responseTask", response);
        }

        [Then(@"the response content is valid")]
        public void ThenTheSuccessResponseContentIsValid()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _scenarioContext.Add("responseUser", responseUser);

            Assert.True(responseUser.IsValid);
        }

        //[Then(@"was created in database")]
        //public async Task ThenWasCreatedInDatabase()
        //{
        //    var responseUser = _scenarioContext.Get<UserContract>("responseUser");
        //    var client = _scenarioContext.Get<HttpClient>("httpClient");
        //    var task = client.GetAsync($"User/{responseUser.UniqueId}");
        //    var response = await task;
        //    var responseContent = response.Content.ReadAsStringAsync().Result;
        //    var userDb = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //    responseUser.Should().BeEquivalentTo(userDb);
        //}

        [Then(@"the response content is invalid with error (.*)")]
        public void ThenTheFailResponseContentIsInvalid(string error)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.False(responseUser.IsValid);
            Assert.Contains(error, responseUser.Errors);
        }
    }
}
