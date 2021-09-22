using System;
using System.Text;
using ChinookASPNETWebAPI.Data.Repositories;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Repositories;
using ChinookASPNETWebAPI.Domain.Supervisor;
using ChinookASPNETWebAPI.Domain.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ChinookASPNETWebAPI.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChinookASPNETWebAPI.API.Configurations
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

        public static void AddCaching(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("ChinookSQLCache");
                options.SchemaName = "dbo";
                options.TableName = "ChinookCache";
            });
        }

        public static void AddIdentity(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

            services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt => {
                    var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuerSigningKey= true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false, 
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    }; 
                });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ChinookContext>();
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.DefaultApiVersion = new ApiVersion( new DateTime( 2020, 9, 22 ) );
                //options.DefaultApiVersion =
                //  new ApiVersion(new DateTime( 2020, 9, 22 ), "LetoII", 1, "Beta");
                options.ReportApiVersions = true;
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public static void AddSwaggerServices(this IServiceCollection services)
        {

            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
        }

        public static void AddApiExplorer(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        }
    }

    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName, 
                    CreateVersionInfo(description));
                options.EnableAnnotations();
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
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
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}