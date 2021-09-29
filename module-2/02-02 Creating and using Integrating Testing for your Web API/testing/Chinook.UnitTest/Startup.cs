using Chinook.DataEF;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Repositories;
using Chinook.Domain.Supervisor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.UnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            var connectionString = configuration.GetConnectionString("ChinookDbWindows");
            services.AddDbContextPool<ChinookContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IAlbumRepository, AlbumRepository>()
                .AddTransient<IArtistRepository, ArtistRepository>()
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<IEmployeeRepository, EmployeeRepository>()
                .AddTransient<IGenreRepository, GenreRepository>()
                .AddTransient<IInvoiceRepository, InvoiceRepository>()
                .AddTransient<IInvoiceLineRepository, InvoiceLineRepository>()
                .AddTransient<IMediaTypeRepository, MediaTypeRepository>()
                .AddTransient<IPlaylistRepository, PlaylistRepository>()
                .AddTransient<ITrackRepository, TrackRepository>()
                .AddTransient<IChinookSupervisor, ChinookSupervisor>()
                .AddTransient<IMemoryCache, MemoryCache>();
        }
    }
}