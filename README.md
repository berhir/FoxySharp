# FoxySharp

This is a collection of samples to work with [FoxyCart](http://www.foxycart.com) in C#.

All projects are build for [.NET Standard](https://github.com/dotnet/standard) and [.NET Core](https://dotnet.github.io) and run on Windows, Linux and macOS.

## Build and run
You can either use Visual Studio (or any other IDE that supports C#/.NET Core) or the new [.NET CLI](https://github.com/dotnet/cli).

Run the following commands to build the projects with the .NET CLI (Windows/Linux/macOS)

```
git clone https://github.com/berhir/FoxySharp.git
cd FoxySharp/src/
git submodule init
git submodule update
dotnet restore
```
Go to one of the project directories containing a project.json file.
Before you can run the project you have to update the appsettings.json with your FoxyCart settings.  
Then start the project with
```
dotnet run
```

## Included projects

### FoxySharp library
Contains base classes that are used by the other projects.
* Sample client to access the FoxyCart Hypermedia API.
* Model classes to de-serialize the retrieved data.
* Classes to work with the XML data feed.

### FoxySharp.Samples.Client
Console application that uses the sample client from the FoxySharp library to read, update and create customers.

### FoxySharp.Samples.Web
ASP.NET Core project with a sample implementation of Single Sing-On and XML data feed.

### FoxySharp.Samples.DataFeedTester
Console application to help you set up and debug your FoxyCart XML data feed implementation.
It's designed to mimic FoxyCart.com and send encrypted and encoded XML to a URL of your choice. It will print out the response that your script gives back, which should be "foxy" if successful.