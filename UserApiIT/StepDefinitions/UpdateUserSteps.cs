using System;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserApiContract;

namespace UserApiIT.StepDefinitions
{
    [Binding]
    public class UpdateUserSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public UpdateUserSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I submit a PUT request (.*)")]
        public void WhenISubmitAPUTRequestUser(string url, Table table)
        {
            var updateUser = table.CreateInstance<UserContract>();
            var content = JsonSerializer.Serialize<UserContract>(updateUser);
            var data = new StringContent(content, Encoding.UTF8, "application/json");

            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var response = client.PutAsync(url, data);

            _scenarioContext.Add("responseTask", response);
        }

        //[Then(@"was updated in database")]
        //public async Task ThenWasUpdatedInDatabase()
        //{
        //    var responseUser = _scenarioContext.Get<UserContract>("responseUser");
        //    var client = _scenarioContext.Get<HttpClient>("httpClient");
        //    var task = client.GetAsync($"User/{responseUser.UniqueId}");
        //    var response = await task;
        //    var responseContent = response.Content.ReadAsStringAsync().Result;
        //    var updatedUser = JsonSerializer.Deserialize<UserContract>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //    Assert.Equal("jorge carvalho", $"{updatedUser?.FirstName} {updatedUser?.LastName}");
        //    Assert.Equal("test@user.com", updatedUser?.Email);
        //}
    }
}
