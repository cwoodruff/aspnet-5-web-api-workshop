---
title: Developing the Web API Business Rules
description: Developing the Web API Business Rules
author: cwoodruff
---
# Developing the Web API Business Rules

## START FROM PREVIOUS MODULE'S END
[Developing the API endpoints](ntier-api-endpoints.md)

## ADD CROSS ORIGIN RESOURCE SHARING (CORS) TO API PROJECT

### ADD ADDCORS() TO SERVICESCONFIGURATION.CS

```csharp
public static void AddCORS(this IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });
}
```

UPDATE STARTUP.CS

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddConnectionProvider(Configuration);
    services.AddAppSettings(Configuration);
    services.ConfigureRepositories();
    services.ConfigureSupervisor();
    services.ConfigureValidators();
    services.AddAPILogging();
    services.AddCORS();
    services.AddHealthChecks();
    services.AddControllers();
}

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```



## ADD FLUENTVALIDATION NUGET TO API AND DOMAIN PROJECTS

```dos
dotnet add package FluentValidation
dotnet add package FluentValidation.AspNetCore
```

## ADD VALIDATORS TO DOMAIN PROJECT

![](api-business-rules/Snag_c547d47.png)

![](api-business-rules/Snag_c547dc4.png)

```csharp
using ChinookASPNETWebAPI.Domain.ApiModels;
using FluentValidation;

namespace ChinookASPNETWebAPI.Domain.Validation
{
    public class AlbumValidator : AbstractValidator<AlbumApiModel>
    {
        public AlbumValidator()
        {
            RuleFor(a => a.Title).NotNull();
            RuleFor(a => a.Title).MinimumLength(3);
            RuleFor(a => a.Title).MaximumLength(160);
            RuleFor(a => a.ArtistId).NotNull();
        }
    }
}
```

## ADD CODE TO CALL VALIDATION IN SUPERVISOR

![](api-business-rules/Snag_c547d66.png)

```csharp
public async Task<AlbumApiModel> AddAlbum(AlbumApiModel newAlbumApiModel)
{
    await _albumValidator.ValidateAndThrowAsync(newAlbumApiModel);

    var album = newAlbumApiModel.Convert();

    album = await _albumRepository.Add(album);
    newAlbumApiModel.Id = album.Id;
    return newAlbumApiModel;
}

public async Task<bool> UpdateAlbum(AlbumApiModel albumApiModel)
{
    await _albumValidator.ValidateAndThrowAsync(albumApiModel);

    var album = await _albumRepository.GetById(albumApiModel.Id);

    if (album is null) return false;
    album.Id = albumApiModel.Id;
    album.Title = albumApiModel.Title;
    album.ArtistId = albumApiModel.ArtistId;

    return await _albumRepository.Update(album);
}
```

## ADD VALIDATORS TO DEPENDENCY INJECTION IN STARTUP IN API PROJECT

```csharp
public static void ConfigureValidators(this IServiceCollection services)
{
    services.AddFluentValidation()
        .AddTransient<IValidator<AlbumApiModel>, AlbumValidator>()
        .AddTransient<IValidator<ArtistApiModel>, ArtistValidator>()
        .AddTransient<IValidator<CustomerApiModel>, CustomerValidator>()
        .AddTransient<IValidator<EmployeeApiModel>, EmployeeValidator>()
        .AddTransient<IValidator<GenreApiModel>, GenreValidator>()
        .AddTransient<IValidator<InvoiceApiModel>, InvoiceValidator>()
        .AddTransient<IValidator<InvoiceLineApiModel>, InvoiceLineValidator>()
        .AddTransient<IValidator<MediaTypeApiModel>, MediaTypeValidator>()
        .AddTransient<IValidator<PlaylistApiModel>, PlaylistValidator>()
        .AddTransient<IValidator<TrackApiModel>, TrackValidator>();
}
```

### ADD CONFIGUREVALIDATORS TO CONFIGURESERVICES

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddConnectionProvider(Configuration);
    services.AddAppSettings(Configuration);
    services.ConfigureRepositories();
    services.ConfigureSupervisor();
    services.ConfigureValidators();
    services.AddAPILogging();
    services.AddCORS();
    services.AddHealthChecks();
    services.AddControllers();
}
```


## ADD ERROR HANDLING IN ACTIONS

```csharp
[Route("api/[controller]")]
[ApiController]
[EnableCors("CorsPolicy")]
public class AlbumController : ControllerBase
{
    private readonly IChinookSupervisor _chinookSupervisor;
    private readonly ILogger<AlbumController> _logger;

    public AlbumController(IChinookSupervisor chinookSupervisor, ILogger<AlbumController> logger)
    {
        _chinookSupervisor = chinookSupervisor;
        _logger = logger;
    }

    [HttpGet]
    [Produces(typeof(List<AlbumApiModel>))]
    public async Task<ActionResult<List<AlbumApiModel>>> Get()
    {
        try  
        {  
            var albums = await _chinookSupervisor.GetAllAlbum();  

            if (albums.Any())  
            {  
                return Ok(albums);  
            }  
            else  
            {  
                return StatusCode((int)HttpStatusCode.NotFound, "No Albums Could Be Found");  
            }  
        }  
        catch (Exception ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Albums");  
        }  
    }

    [HttpGet("{id}", Name = "GetAlbumById")]
    public async Task<ActionResult<AlbumApiModel>> Get(int id)
    {
        try  
        {  
            var album = await _chinookSupervisor.GetAlbumById(id);  

            if (album != null)  
            {  
                return Ok(album);  
            }  
            else  
            {  
                return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");  
            }  
        }  
        catch (Exception ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController GetById action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get Album By Id");  
        }
    }

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<AlbumApiModel>> Post([FromBody] AlbumApiModel input)
    {
        try  
        {  
            if (input == null)  
            {  
                return StatusCode((int)HttpStatusCode.BadRequest, "Given Album is null");
            }  
            else  
            {
                return Ok(await _chinookSupervisor.AddAlbum(input));
            }  
        }
        catch (ValidationException  ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController Add Album action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Albums");  
        }
        catch (Exception ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController Add Album action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Albums");  
        }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<AlbumApiModel>> Put(int id, [FromBody] AlbumApiModel input)
    {
        try  
        {  
            if (input == null)  
            {  
                return StatusCode((int)HttpStatusCode.BadRequest, "Given Album is null");
            }  
            else  
            {
                return Ok(await _chinookSupervisor.UpdateAlbum(input));
            }  
        }
        catch (ValidationException  ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController Update Album action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Albums");  
        }
        catch (Exception ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController Update Album action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Albums");  
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try  
        {  
            return Ok(await _chinookSupervisor.DeleteAlbum(id)); 
        }  
        catch (Exception ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController Delete action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Delete Album");  
        }
    }

    [HttpGet("artist/{id}")]
    public async Task<ActionResult<List<AlbumApiModel>>> GetByArtistId(int id)
    {
        try  
        {  
            var albums = await _chinookSupervisor.GetAlbumByArtistId(id);  

            if (albums.Any())  
            {  
                return Ok(albums);  
            }  
            else  
            {  
                return StatusCode((int)HttpStatusCode.NotFound, "No Albums Could Be Found for the Artist");  
            }  
        }  
        catch (Exception ex)  
        {  
            _logger.LogError($"Something went wrong inside the AlbumController Get By Artist action: {ex}");  
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Albums for Artist");  
        }  
    }
}
```

## ERROR HANDLING AND RETURNING PROBLEM DETAILS

