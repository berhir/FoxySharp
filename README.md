# FoxySharp

This is a collection of samples to work with [FoxyCart](http://www.foxycart.com) in C#.

All projects are built for [.NET Standard](https://github.com/dotnet/standard) and [.NET Core](https://dotnet.github.io) and run on Windows, Linux and macOS.

## Build and run
You can either use Visual Studio (or any other IDE that supports C#/.NET Core) or the new [.NET CLI](https://github.com/dotnet/cli).

Run the following commands to build the projects with the .NET CLI (Windows/Linux/macOS)

```
git clone https://github.com/berhir/FoxySharp.git
cd FoxySharp/src/
dotnet restore
```
Before you can start the projects you have to update the appsettings.json with your FoxyCart settings (see below).  
Then go to the root directory of the project you want to start and type
```
dotnet run
```

## Projects

### FoxySharp library
Contains base classes that are used by the other projects.
* Sample client to access the FoxyCart Hypermedia API.
* Model classes to deserialize the retrieved data.
* Classes to work with the XML data feed.

### FoxySharp.Samples.Client
Console application that uses the sample client from the FoxySharp library to read, update and create customers.

Required settings in the appsettings.json:
```json
{
  "FoxyCart": {
    "ApiEndpoint": "https://api.foxycart.com",
    "ClientId": "",
    "ClientSecret": "",
    "RefreshToken": ""
  }
}
```

* ApiEndpoint: the URL of the FoxyCart API (you can also use the sandbox version: https://api-sandbox.foxycart.com)
* ClientId, ClientSecret, RefreshToken: read the [documentation](https://api.foxycart.com/docs/authentication/client_creation) to find out how to generate these values from within the Foxy administration.

### FoxySharp.Samples.Web
ASP.NET Core project with a sample implementation of Single Sign-On and XML data feed.

Required settings in the appsettings.json:
```json
{
  ...
  "FoxyCart": {
    "StoreUrl": "https://YOUR-STORE.foxycart.com",
    "ApiKey": "TQ8Tezm6XluFkyYY7a9aPeRcvfadnMBbxDUG7cGUn5uDtisrnvv3Dc93labD",
    "ApiEndpoint": "https://api.foxycart.com",
    "ClientId": "",
    "ClientSecret": "",
    "RefreshToken": ""
  }
}
```

* StoreUrl: used by the Single Sign-On implementation to redirect the customer to your store.
* ApiKey: used by the SSO implementation to create a secure hash and by the data feed implementation to decrypt the data.
* ClientId, ClientSecret and RefreshToken: only required if you want to use the client to access the FoxyCart API (see client sample for more details).

### FoxySharp.Samples.DataFeedTester
Console application to help you set up and debug your FoxyCart XML data feed implementation.
It's designed to mimic FoxyCart.com and send encrypted and encoded XML to a URL of your choice. It will print out the response that your script gives back, which should be "foxy" if successful.

Required settings in the appsettings.json:
```json
{
  "FoxyCart": {
    "ApiKey": "TQ8Tezm6XluFkyYY7a9aPeRcvfadnMBbxDUG7cGUn5uDtisrnvv3Dc93labD",
    "DataFeedUrl": "http://localhost:5000/FoxyCart/DataFeed"
  }
}
```

* ApiKey: used to encrypt the XML data.
* DataFeedUrl: the URL of your data feed endpoint.