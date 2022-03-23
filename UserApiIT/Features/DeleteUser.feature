Feature: Delete user profile using UserApi

@delete
Scenario: Successfully delete the added user profile
	Given User endpoint is available
	And User exists
	| UniqueId | FirstName | LastName | Email                | MobileNumer | Address        | PostCode | City  | DateOfBirth                 | Errors |
	|          | test      | user     | user.delete@test.com | 935469874   | Rua Raul Gomes | 4715011  | Braga | 1988-10-30 00:00:00.0000000 |        |
	When I submit a DELETE request /User?uniqueId=
	Then I receive a response
	And the http response status is OK
	And the response content is valid
	#And was deleted in database