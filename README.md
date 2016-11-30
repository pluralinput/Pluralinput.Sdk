# Pluralinput.Sdk [![Build status](https://ci.appveyor.com/api/projects/status/4r7rjw82tn2oc5mf/branch/master?svg=true)](https://ci.appveyor.com/project/chgl/pluralinput-sdk/branch/master)
This is complete rewrite of the previous, non-open Pluralinput SDK.
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

## TODO
- [ ] add package to nuget.org
- [x] check x64/x86 compatibility issues
- [ ] check .NET Core compatibility
- [x] add Unity sample project
- [ ] add WinForms sample project
- [ ] add WPF sample project
- [ ] add UWP sample project
