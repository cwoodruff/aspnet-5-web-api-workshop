using System;
using System.Linq;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Data.Data;
using ChinookASPNETWebAPI.Data.Repositories;
using ChinookASPNETWebAPI.Domain.Supervisor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Supervisor
{
    public class ArtistSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly ArtistRepository _artistRepo;
        private readonly ChinookContext _context;

        public ArtistSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _artistRepo = new ArtistRepository(_context);
            var artistRepo = new ArtistRepository(_context);
            _super = new ChinookSupervisor(null, _artistRepo, null, null, 
                null, null, null, null, 
                null, null, null, null,
                null, null, null, null, null,
                null, null, null, new MemoryCache(new MemoryCacheOptions()), 
                null);
        }
        
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _artistRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task ArtistGetAll()
        {
            // Act
            var artists = (await _super.GetAllArtist()).ToList();

            // Assert
            Assert.True(artists.Count > 1, "The number of artists was not greater than 1");
        }
    }
}