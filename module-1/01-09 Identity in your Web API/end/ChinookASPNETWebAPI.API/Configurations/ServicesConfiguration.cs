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
using ChinookASPNETWebAPI.Domain.Identity;

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
    }
}