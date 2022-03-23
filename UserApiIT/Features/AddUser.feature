Feature: Add user profile using UserApi

@add
Scenario: Successfully add user profile using UserApi
	Given User endpoint is available
	When I submit a POST request /User
	| UniqueId | FirstName | LastName | Email             | MobileNumer | Address        | PostCode | City  | DateOfBirth                 | Errors |
	|          | test      | user     | user.add@test.com | 914567894   | Rua Raul Gomes | 4715011  | Braga | 1988-10-30 00:00:00.0000000 |        |
	Then I receive a response
	And the http response status is OK
	And the response content is valid
	#And was created in database

Scenario: Unsuccessfully add user profile with underage user
	Given User endpoint is available
	When I submit a POST request /User
	| UniqueId | FirstName | LastName | Email                | MobileNumer | Address        | PostCode | City  | DateOfBirth                 | Errors |
	|          | test      | user     | bad.user.add@test.co | 914568932   | Rua Raul Gomes | 4715011  | Braga | 2010-10-30 00:00:00.0000000 |        |
	Then I receive a response
	And the http response status is BadRequest
	And the response content is invalid with error User must be +18

Scenario: Unsuccessfully add user profile with invalid email
	Given User endpoint is available
	When I submit a POST request /User
	| UniqueId | FirstName | LastName | Email     | MobileNumer | Address        | PostCode | City  | DateOfBirth                 | Errors |
	|          | test      | user     | jorge.com | 918214796   | Rua Raul Gomes | 4715011  | Braga | 1988-10-30 00:00:00.0000000 |        |
	Then I receive a response
	And the http response status is BadRequest
	And the response content is invalid with error Email is Invalid
