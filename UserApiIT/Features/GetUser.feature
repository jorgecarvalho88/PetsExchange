Feature: Get user profile from UserApi

@get
Scenario: Successfully get user profile using UserApi
	Given User endpoint is available
	When I submit a GET request /User/4fffd014-fb5e-4809-b758-9de71b30e25f
	Then I receive a response
	And the http response status is OK
	And the success response content is
	| UniqueId                             | FirstName | LastName | Email           | MobileNumer | Address        | PostCode | City  | DateOfBirth                 | Errors |
	| 4fffd014-fb5e-4809-b758-9de71b30e25f | jorge     | carvalho | jorge@gmail.com | 918478026   | Rua Raul Gomes | 4715     | Braga | 1988-10-30 00:22:10.2510000 |        |

Scenario: Unsuccessfully get user profile using UserApi
	Given User endpoint is available
	When I submit a GET request /User/f155650c-956e-48f2-8884-c4f42233a587
	Then I receive a response
	And the http response status is BadRequest
	And the fail response content is
	| UniqueId                             | FirstName | LastName | Email | MobileNumer | Address | PostCode | City | DateOfBirth         | Errors         |
	| 00000000-0000-0000-0000-000000000000 |           |          |       |             |         |          |      | 0001-01-01T00:00:00 | User not found |