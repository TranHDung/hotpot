# ngx-admin Backend Bundle instruction


## Intro

This is readme and instructions how start using backend bundle from Akveo. 
Backend bundle is integrated solution of Backend Code and Frontend code. 
Backend code plays mostly API role, giving data to the client side as REST API. 

## Running Instruction

 - open `*WebApiCore/appsettings.Development.json` and put correct data base connection string
 
 ```json
"ConnectionStrings": {
    "localDb": "<your connection string here>>"
  }
```
  
  you can use localDb or any other cloud SQL Server
  
 - Select *WebApiCore project and make it default project in Visual Studio
 - hit F5 and start debugging it
 - use URL where backend is started as environment variable for frontend part to access API. Check frontend instruction. Sample of backend api url is: `http://localhost:12345/api`. Please check properties of WebApi projects to get the actual URL of api.
 - start frontend part according to it's instruction

## Tech Stack

This Code Bundle has backend and frontend parts.

Backend Part uses following libraries and frameworks:

 - .NET Core 3.0 *
 - .NET Standard 2.0
 - ASP.NET Core Web API
 - ASP.NET Identity
 - Owin
 - Swagger **
 - AutoMapper
 - Entity Framework Core
 - Entity Framework Plus

\* Newtonsoft.Json is used as json-serializer instead of a new System.Text.Json to prevent potential compatibility issues.
For using System.Text.Json remove  service registration 'AddNewtonsoftJson' from startup class and next packages:
- Newtonsoft.Json;
- Microsoft.AspNetCore.Mvc.NewtonsoftJson.

\** Package Swashbuckle.AspNetCore for Swagger is version 5 RC-4. It is the last stable version for .net core 3, but not a final release. Should be upgraded later.

## API Documentation 

You can check API documentation by running api and accessing <api_url>/swagger/ui/index#/ link.
To use swagger with token authentication please follow these steps:

 - open swagger link <api_url>/swagger/ui/index#/ while running api
 - expand **Auth** controller and open POST /api/auth/login action
 - put correct user info into loginDto field (there is sample in swagger) and click 'try out'
 - when received response with token, copy token (ctrl+c)
 - there is input 'api_key' at top right corner. Paste there token in format: 'Bearer <token>' and click 'Explore'
 - after UI was refreshed, you can try any requests, token will be added there

#### Basic Code Structure

Code is organized in following structure

 - frontend  - *contains code of frontend application*
 - docs  - *contains licenses and instruction*
 - backend
	 - BundleNet*.sln
	 - Common
		 - Common.WebApi - *contains base code and settings for web api*
		 - ....
	 - <ECommerce/IoT>
		 - <ECommerce/IoT>.WebApi
			 - web.config
			 - Controllers
			 - Startup.cs
		 - ...

## Test User / Password

You can use these test users for application testing:

1. user@user.user / 12345
2. admin@admin.admin / !2e4S


## Settings to change for each project

 - secret key for token generation. You can use this code to generate new secret key:

```
Convert.ToBase64String(new System.Security.Cryptography.HMACSHA256().Key)
```

 - CORS origins
 - connection string to data base in appsettings.json
 - api url in environment.ts
 - logging settings is needed (Serilog)

## Support
Please post issues in [Bundle Support Issue Tracker](https://github.com/akveo/ngx-admin-bundle-support/issues)
