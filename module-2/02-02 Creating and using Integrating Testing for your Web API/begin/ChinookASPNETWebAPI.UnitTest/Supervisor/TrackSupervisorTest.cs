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
    public class TrackSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly TrackRepository _trackRepo;
        private readonly ChinookContext _context;

        public TrackSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _trackRepo = new TrackRepository(_context);
            _super = new ChinookSupervisor(null, null, null, null, 
                null, null, null, null, 
                null, _trackRepo, null, null,
                null, null, null, null, null,
                null, null, null, new MemoryCache(new MemoryCacheOptions()), 
                new MemoryDistributedCache(Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions())));
        }
        
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _trackRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task TrackGetAll()
        {
            // Act
            var tracks = (await _super.GetAllTrack()).ToList();

            // Assert
            Assert.True(tracks.Count > 1, "The number of tracks was not greater than 1");
        }
    }
}