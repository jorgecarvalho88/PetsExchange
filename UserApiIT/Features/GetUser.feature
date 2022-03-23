Feature: Get user profile from UserApi

@get
Scenario: Successfully get user profile using UserApi
	Given User endpoint is available
	When I submit a GET request /User/id/b125195a-691a-4b5c-bbe5-cd9ccf6565c7
	Then I receive a response
	And the http response status is OK
	And the success response content is
	| UniqueId                             | FirstName | LastName | Email             | MobileNumer | Address     | PostCode | City  | DateOfBirth                 | Errors |
	| b125195a-691a-4b5c-bbe5-cd9ccf6565c7 | john      | doe      | john.doe@mail.com | 987654321   | Rua de tras | 4700010  | Braga | 1980-03-22 22:11:10.1760000 |        |

Scenario: Unsuccessfully get user profile using UserApi
	Given User endpoint is available
	When I submit a GET request /User/id/f155650c-956e-48f2-8884-c4f42233a587
	Then I receive a response
	And the http response status is BadRequest
	And the fail response content is
	| UniqueId                             | FirstName | LastName | Email | MobileNumer | Address | PostCode | City | DateOfBirth         | Errors         |
	| 00000000-0000-0000-0000-000000000000 |           |          |       |             |         |          |      | 0001-01-01T00:00:00 | User not found |