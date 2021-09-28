---
title: Formatting Response Data in your API
description: Formatting Reponse Data in your API
author: cwoodruff
---
# Formatting Response Data in your API

## START FROM PREVIOUS MODULE'S END
[Documenting your API with OpenAPI](documenting-with-openapi.md)

## Format-specific Action Results

### JSON-formatted Data Response

```csharp
[HttpGet]
[Produces(typeof(List<AlbumApiModel>))]
public async Task<ActionResult<List<AlbumApiModel>>> Get()
{
	return Ok(await _chinookSupervisor.GetAllAlbum());
}
```

### String-formatted Data Response

```csharp
[HttpGet("About")]
public ContentResult About()
{
    return Content("An API listing authors of docs.asp.net.");
}
```

```csharp
[HttpGet("version")]
public string Version()
{
    return "Version 1.0.0";
}
```

## By default, ASP.NET 5 supports the following formats for responses:

* application/json
* text/json
* text/plain

## Web Browser & Response Formats

### When ASP.NET 5 Web API detects a browser calling an endpoint what happens?

* The Accept header is ignored.
* The content is returned in JSON, unless otherwise configured.

### To configure an app to honor browser accept headers:

```csharp
services.AddControllers(options =>
{
	options.RespectBrowserAcceptHeader = true; // false by default
});
```

### If no formatter is found that can satisfy the client's request, ASP.NET Core will:
Returns 406 Not Acceptable if MvcOptions.ReturnHttpNotAcceptable is set to true

## Restrict Response Formats

### To restrict the response formats, apply the [Produces] filter. Like most Filters, [Produces] can be applied at the action, controller, or global scope:

```csharp
[HttpPost]
[Produces("application/json")]
[Consumes("application/json")]
public async Task<ActionResult<ArtistApiModel>> Post([FromBody] ArtistApiModel input)
{
```

## Response Format URL Mappings

### The mapping from request path should be specified in the route the API is using. For example:

```csharp
[Route("api/[controller]")]
[ApiController]
[FormatFilter]
public class AlbumsController : ControllerBase
{
    [HttpGet("{id}.{format?}")]
    public Album Get(int id)
    {
```

| Route                | Formatter                          |
|----------------------|------------------------------------|
| /api/Album/5      | The default output formatter       |
| /api/Album/5.json | The JSON formatter (if configured) |
| /api/Album/5.xml  | The XML formatter (if configured)  |
