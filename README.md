[Protocol Buffers](https://developers.google.com/protocol-buffers) are Google's language-neutral, platform-neutral, extensible mechanism for serializing structured data – think XML, but smaller, faster, and simpler. 

## Byndyusoft.Net.Http.ProtoBuf
[![(License)](https://img.shields.io/github/license/Byndyusoft/Byndyusoft.Net.Http.ProtoBuf.svg)](LICENSE.txt)
[![Nuget](http://img.shields.io/nuget/v/Byndyusoft.Net.Http.ProtoBuf.svg?maxAge=10800)](https://www.nuget.org/packages/Byndyusoft.Net.Http.ProtoBuf/) [![NuGet downloads](https://img.shields.io/nuget/dt/Byndyusoft.Net.Http.ProtoBuf.svg)](https://www.nuget.org/packages/Byndyusoft.Net.Http.ProtoBuf/) 

Provides extension methods for System.Net.Http.HttpClient and System.Net.Http.HttpContent that perform automatic serialization and deserialization using ProtoBuf.

This package actually depends on ```Microsoft.Net.Http```, and extends the ```HttpClient``` with ```ProtoBuf```
features that you would likely need to talk to a RESTful service such as ASP.NET Web API.
Package operates in the ```System.Net.Http``` namespace and adds some handy extension methods to ```HttpClient``` and ```HttpContent```.

So for example:

```csharp
using (var client = new HttpClient())
{
    var product = await client.GetFromProtoBufAsync<Product>("http://localhost/api/products/1");
}
```
or
```csharp
using (var client = new HttpClient())
{
    var response = await _client.GetAsync("http://localhost/api/products/1");
    response.EnsureSuccessStatusCode();
    var product = await response.Content.ReadFromProtoBufAsync<Product>();
}
```
or
```csharp
using (var client = new HttpClient())
{
    var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/products/1");
    request.Content = ProtoBufContent.Create(new Product());
    var response = await _client.SendAsync(request);
    response.EnsureSuccessStatusCode();
}
```

If you tried to just use ```Microsoft.Net.Http```, the ```GetFromProtoBufAsync``` method wouldn't be available to you, and you'd only be able to read the content 
as raw data such as bytes or string, and have to do the serializing / de-serializing yourself.

You also get extension methods to PUT / POST back to the service in ```ProtoBuf``` format without having to do that yourself:

```csharp
await client.PutAsProtoBufAsync("http://localhost/api/products/1", product);
await client.PostAsProtoBufAsync("http://localhost/api/products/1", product);
```

### Installing

```shell
dotnet add package Byndyusoft.Net.Http.ProtoBuf
```

***

## Byndyusoft.Net.Http.Formatting.ProtoBuf

[![(License)](https://img.shields.io/github/license/Byndyusoft/Byndyusoft.Net.Http.Formatting.ProtoBuf.svg)](LICENSE.txt)
[![Nuget](http://img.shields.io/nuget/v/Byndyusoft.Net.Http.Formatting.ProtoBuf.svg?maxAge=10800)](https://www.nuget.org/packages/Byndyusoft.Net.Http.Formatting.ProtoBuf/) [![NuGet downloads](https://img.shields.io/nuget/dt/Byndyusoft.Net.Http.Formatting.ProtoBuf.svg)](https://www.nuget.org/packages/Byndyusoft.Net.Http.Formatting.ProtoBuf/) 


This package adds `ProtoBufMediaTypeFormatter` class for formatting `HttpClient` requests and responses.

So for example:

```csharp
using (var client = new HttpClient())
{
	var formatter = new ProtoBufMediaTypeFormatter();
	var request = new SearchProductRequest { Name = 'iphone', OrderBy = 'id' };
	var content = new ObjectContent<SearchProductRequest>(request, formatter);
	var response = await client.PostAsync("http://localhost/api/products:search");
	var products = await response.Content.ReadAsAsync<Product[]>(new[] {formatter});
}
```

### Installing

```shell
dotnet add package Byndyusoft.Net.Http.Formatting.ProtoBuf
```

***

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For the contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET Core (version 3.1 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/download/dotnet-core/3.1).

## General folders layout

### src
- source code

### tests

- unit-tests

## Package development lifecycle

- Implement package logic in `src`
- Add or addapt unit-tests (prefer before and simultaneously with coding) in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)