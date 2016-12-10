# Pluralinput.Sdk [![Build status](https://ci.appveyor.com/api/projects/status/sncj8u6naeob4wbt?svg=true)](https://ci.appveyor.com/project/chgl/pluralinput-sdk)
This is complete rewrite of the previous, non-open Pluralinput SDK.

## Installation
[![NuGet](https://img.shields.io/nuget/v/Pluralinput.Sdk.svg?style=flat-square)](https://www.nuget.org/packages/Pluralinput.Sdk/)

```
PM> Install-Package Pluralinput.Sdk
```
or install via [VS Package Management window](https://docs.nuget.org/ndocs/tools/package-manager-ui).

## Code samples

### Basic usage
```csharp

using Pluralinput.Sdk;

...

// the InputManager initializes the SDK and should only be created once per application
var im = new InputManager();
// returns a list of all mouse devices
var mice = im.Devices.Mice;
// listen to the first mouse's button up event
mice.First().ButtonUp += (o, e) =>
{
    Console.WriteLine($"{o}: ButtonUp {e.Button}");
};
```
Make sure to check out the [samples](/samples)-folder for more.

## TODO
- [x] add package to nuget.org
- [x] check x64/x86 compatibility issues
- [ ] check .NET Core compatibility
- [x] add Unity sample project
- [ ] add WinForms sample project
- [ ] add WPF sample project
- [ ] add UWP sample project
