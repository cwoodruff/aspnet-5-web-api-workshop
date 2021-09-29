using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Repositories;
using Chinook.Domain.Supervisor;
using Chinook.Domain.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

//using Chinook.DataJson.Repositories;
//using Chinook.DataDapper.Repositories;

namespace Chinook.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAlbumRepository, AlbumRepository>()
                .AddScoped<IArtistRepository, ArtistRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IGenreRepository, GenreRepository>()
                .AddScoped<IInvoiceRepository, InvoiceRepository>()
                .AddScoped<IInvoiceLineRepository, InvoiceLineRepository>()
                .AddScoped<IMediaTypeRepository, MediaTypeRepository>()
                .AddScoped<IPlaylistRepository, PlaylistRepository>()
                .AddScoped<ITrackRepository, TrackRepository>();
        }
        
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

        public static void ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<IChinookSupervisor, ChinookSupervisor>();
        }

        public static void AddAPILogging(this IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)
            );
        }

        public static void AddCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCaching();
        }

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

        public static void AddVersioningServices(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
        }

        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Chinook Music Store API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Chris Woodruff",
                        Email = string.Empty,
                        Url = new Uri("https://chriswoodruff.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                c.EnableAnnotations();
            });
        }
    }
}