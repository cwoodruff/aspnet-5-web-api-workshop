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
    public class MediaTypeSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly MediaTypeRepository _mediaTypeRepo;
        private readonly ChinookContext _context;

        public MediaTypeSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _mediaTypeRepo = new MediaTypeRepository(_context);
            _super = new ChinookSupervisor(null, null, null, null, 
                null, null, null, _mediaTypeRepo, 
                null, null, null, null,
                null, null, null, null, null,
                null, null, null, new MemoryCache(new MemoryCacheOptions()), 
                null);
        }
        
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _mediaTypeRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task MediaTypeGetAll()
        {
            // Act
            var mediaTypes = (await _super.GetAllMediaType()).ToList();

            // Assert
            Assert.True(mediaTypes.Count > 1, "The number of media types was not greater than 1");
        }
    }
}