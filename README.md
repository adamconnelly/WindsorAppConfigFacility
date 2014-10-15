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

## Other Features

### Prefixes
You can specify a prefix that should be used for getting settings from the settings store. What this allows you to do is split your app settings up into smaller, more specific settings interfaces.

For example, say we have the following settings:

```xml
<appSettings>
  <add key="Api.Url" value="http://api.someservice.com"/>
  <add key="Api.Token" value="ABCD1234"/>
  <add key="Email.SenderAddress" value="sender@email.com"/>
  <add key="Email.SmtpServer" value="smtp.email.com"/>
  <add key="Email.IsEnabled" value="true"/>
</appSettings>
```

And you create the following two interfaces to access your settings:

```csharp
public interface IApiConfig
{
    string Url { get; }
    string Token { get; }
}

public interface IEmailConfig
{
    string SenderAddress { get; }
    string SmtpServer { get; }
    bool IsEnabled { get; }
}
```

You can configure your container as follows:

```csharp
var container = new WindsorContainer()
    .AddFacility<AppConfigFacility>();

container.Register(
    Component.For<IApiConfig>().FromAppConfig(c => c.WithPrefix("Api.")),
    Component.For<IEmailConfig>().FromAppConfig(c => c.WithPrefix("Email."));
```

### Azure
If you want to get your settings from Azure instead of an app.config or web.config file, install the WindsorAppConfig.Azure nuget package and just register the facility as follows:

```csharp
container.AddFacility<AppConfigFacility>(c => c.FromAzure());
```

What this does is alters the facility so that it uses ```CloudConfigurationManager.GetSetting("MyKey")``` to access the settings.

### Caching
By default the facility doesn't cache any of your settings. What this means is that it'll go back to the underlying data source (e.g. the web.config file or Azure CloudConfigurationManager) each time you get a setting. If you want it to cache your settings so it only gets them once, you can configure the facility as follows:

```csharp
container.AddFacility<AppConfigFacility>(f => f.CacheSettings());
```

## Advanced Stuff
TODO
