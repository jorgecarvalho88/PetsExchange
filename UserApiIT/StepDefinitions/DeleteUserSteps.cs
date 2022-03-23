using System;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserApiContract;

namespace UserApiIT.StepDefinitions
{
    [Binding]
    public class DeleteUserSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public DeleteUserSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"User exists")]
        public void GivenUserExists(Table table)
        {
            var newUser = table.CreateInstance<UserContract>();
            var content = JsonSerializer.Serialize<UserContract>(newUser);
            var data = new StringContent(content, Encoding.UTF8, "application/json");

            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var task = client.PostAsync("/User", data);

            var response = task.GetAwaiter().GetResult();
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.True(responseUser?.IsValid);

            _scenarioContext.Add("existingUser", responseUser);
        }


        [When(@"I submit a DELETE request (.*)")]
        public void WhenISubmitADELETERequestUser(string url)
        {
            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var createdUser = _scenarioContext.Get<UserContract>("existingUser");
            var response = client.DeleteAsync($"{url}{createdUser.UniqueId}");

            _scenarioContext.Add("responseTask", response);
        }

        //[Then(@"was deleted in database")]
        //public async Task ThenWasDeletedInDatabase()
        //{
        //    var responseUser = _scenarioContext.Get<UserContract>("responseUser");
        //    var client = _scenarioContext.Get<HttpClient>("httpClient");
        //    var task = client.GetAsync($"User/{responseUser.UniqueId}");
        //    var response = await task;

        //    var responseContent = response.Content.ReadAsStringAsync().Result;
        //    var createdUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    Assert.Contains("User not found", createdUser?.Errors);
        //}
    }
}
