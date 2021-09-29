using System;
using System.Linq;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Data.Data;
using ChinookASPNETWebAPI.Data.Repositories;
using ChinookASPNETWebAPI.Domain.Entities;
using ChinookASPNETWebAPI.Domain.Supervisor;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Supervisor
{
    public class AlbumSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly AlbumRepository _albumRepo;
        private readonly ChinookContext _context;

        public AlbumSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _albumRepo = new AlbumRepository(_context);
            var artistRepo = new ArtistRepository(_context);
            _super = new ChinookSupervisor(_albumRepo, artistRepo, null, null, 
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
            _albumRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task GetAllAlbum_GivenTwoAlbumsInTheDatabase_ReturnsBoth()
        {
            var album1 = new Album { Id = 12 };
            var album2 = new Album { Id = 123 };

            // Arrange
            _context.Albums.Add(album1);
            _context.Albums.Add(album2);
            await _context.SaveChangesAsync();

            // Act
            var albums = (await _super.GetAllAlbum()).ToList();

            // Assert
            albums.Count.Should().Be(2);
            albums.Should().Contain(x => x.Id == 12);
            albums.Should().Contain(x => x.Id == 123);
        }
    }
}