---
title: Consuming ASP.NET Web API in .NET
description: Consuming ASP.NET Web API in .NET
author: cwoodruff
---
# Consuming ASP.NET Web API in .NET

## CREATE NEW CONSOLE PROJECT

```dos
dotnet new console --name ChinookAPIClient
```

ADD the following async method to the Program class 

```csharp
private static async Task ProcessRepositories()
{
}
```

## REPLACE MAIN METHOD

```csharp
static async Task Main(string[] args)
{
    await ProcessRepositories();
}
```

## CREATE NEW STATIC INSTANCE OF HttpClinet

```csharp
namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            //...
        }
    }
}
```

## ADD API CALL TO ProcessRepositories

```csharp
private static async Task ProcessRepositories()
{
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("User-Agent", ".NET Console");

    var stringTask = client.GetStringAsync("https://localhost:44320/api/v1/Customer");

    var msg = await stringTask;
    Console.Write(msg);
}
```
