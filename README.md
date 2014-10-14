# Windsor App Config Facility
A Windsor facility for automatically implementing interfaces to access application settings.

[![Build Status](https://travis-ci.org/adamconnelly/WindsorAppConfigFacility.svg?branch=master)](https://travis-ci.org/adamconnelly/WindsorAppConfigFacility)

License: [MIT](http://www.opensource.org/licenses/mit-license.php)

### NuGet: TODO

## What's it for?
Sometimes you need to use settings to alter how your application behaves. The default way to do this is using AppSettings via an app.config or web.config file. Accessing the AppSettings dictionary directly isn't great for unit testing because the objects being tested then have an external dependency that may not be obvious, and may rely on magic strings to work.

What you can do instead is define an interface to hold your settings, and inject that into your class. This way you can mock it for unit testing.

You can manually create an object that implements this interface at runtime, but this can get a bit tedious when you have to write the same boiler plate code over and over as you add new settings. What this facility does is automatically implements that class for you so you don't have to.

## Getting Started

Say you have the following settings:

```xml
<appSettings>
  <add key="ApiUrl" value="http://api.someservice.com"/>
  <add key="ApiToken" value="ABCD1234"/>
</appSettings>
```

You define the following interface to access the settings:

```csharp
public interface IAppConfig
{
    string ApiUrl { get; }
    string ApiToken { get; }
}
```

Now create your container, add the facility, and register your config component:

```csharp
var container = new WindsorContainer();

container.AddFacility<AppConfigFacility>();

container.Register(Component.For<IAppConfig>().FromAppConfig());
```

Now you can inject IAppConfig into other classes, just like you would for any other component registered in Windsor.

## Prefixes
TODO

## Azure
TODO

## Caching
TODO

## Advanced Stuff
TODO
