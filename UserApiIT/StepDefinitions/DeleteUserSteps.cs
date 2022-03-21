using System;
using TechTalk.SpecFlow;
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

        [When(@"I submit a DELETE request (.*)")]
        public void WhenISubmitADELETERequestUser(string url)
        {
            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var createdUser = _scenarioContext.Get<UserContract>("responseUser");
            var response = client.DeleteAsync($"{url}{createdUser.UniqueId}");

            _scenarioContext.Add("responseTask", response);
        }

        [Then(@"was deleted in database")]
        public void ThenWasDeletedInDatabase()
        {
            var responseUser = _scenarioContext.Get<UserContract>("responseUser");
            var client = _scenarioContext.Get<HttpClient>("httpClient");
            var userDb = client.GetAsync($"User/{responseUser.UniqueId}");

            Assert.Contains("User not found", "User not found");
        }
    }
}
