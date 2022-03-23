Feature: Update user profile using User Api

@update
Scenario: Successfully update a user profile
	Given User endpoint is available
	When I submit a PUT request /User
		| UniqueId                             | FirstName | LastName | Email         | MobileNumer | Address     | PostCode | City  | DateOfBirth                 | Errors |
		| b125195a-691a-4b5c-bbe5-cd9ccf6565c7 | jorge     | carvalho | test@user.com | 987654321   | Rua de tras | 4700010  | Braga | 1980-03-22 22:11:10.1760000 |        |
	Then I receive a response
	And the http response status is OK
	And the response content is valid
	#And was updated in database
