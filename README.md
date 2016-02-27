# stormpath-aspnet-webapi-bearer-example

This examples demonstrates how to use Stormpath to secure ASP.NET Web API 2 controllers using OAuth2.0, JWTs, and Bearer authentication.

## How to run the sample

### Setting up the server:
* Sign up for [Stormpath](stormpath.com) if you don't have an account
* Create at least one test user account in "My Application"
* Download an API credentials file (`apiKey.properties`) from Stormpath and place it somewhere safe
* Clone the repository
* Open the solution file
* Edit the file `StormpathConfig.cs` and update the Stormpath Application href, and the path to your API key file
* Build and run the solution - the server will start listening for requests

### Getting an access token:
Use a tool like Fiddler to create a request like
```
POST api/token
Content-Type: application/x-www-form-urlencoded
grant_type=password&username=test%40test.foo&password=Changeme123
```

You should get back a JSON response:
```
{
"access_token":"long-jwt-here",
"refresh_token":"another-long-jwt-here",
"token_type":"Bearer",
"expires_in":3600
}
```

###Accessing a protected route:
Once you have an access token, you can use it to make an authenticated request:
```
GET api/protected
Authorization: Bearer long-jwt-here
```

A valid access token will return `200 OK`. :tada:

### Refreshing the access token:
If the access token expires, you can use the refresh token to generate a new access token:
```
POST api/token
Content-Type: application/x-www-form-urlencoded
grant_type=refresh_token&refresh_token=another-long-jwt-here
```

## How to get help

If you have trouble running this sample, feel free to email [support@stormpath.com](mailto:support@stormpath.com) and mention this repository. Someone will be happy to help! :)
