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
    public class PlayListSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly PlaylistRepository _playlistRepo;
        private readonly ChinookContext _context;

        public PlayListSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _playlistRepo = new PlaylistRepository(_context);
            _super = new ChinookSupervisor(null, null, null, null, 
                null, null, null, null, 
                _playlistRepo, null, null, null,
                null, null, null, null, null,
                null, null, null, new MemoryCache(new MemoryCacheOptions()), 
                null);
        }
        
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _playlistRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task PlayListGetAll()
        {
            // Act
            var playLists = (await _super.GetAllPlaylist()).ToList();

            // Assert
            Assert.True(playLists.Count > 1, "The number of play lists was not greater than 1");
        }
    }
}