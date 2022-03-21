Feature: Delete user profile using UserApi

@delete
Scenario: Successfully delete the added user profile
	When I submit a DELETE request /User?uniqueId=
	Then I receive a response
	And the http response status is OK
	And the response content is valid
	And was deleted in database