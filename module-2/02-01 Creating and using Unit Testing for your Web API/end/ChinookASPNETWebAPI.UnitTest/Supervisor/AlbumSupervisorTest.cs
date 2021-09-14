using System;
using System.Linq;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Data.Data;
using ChinookASPNETWebAPI.Data.Repositories;
using ChinookASPNETWebAPI.Domain.Entities;
using ChinookASPNETWebAPI.Domain.Supervisor;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
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
            _context.SaveChanges();

            // Act
            var albums = (await _super.GetAllAlbum()).ToList();

            // Assert
            albums.Count.Should().Be(2);
            albums.Should().Contain(x => x.Id == 12);
            albums.Should().Contain(x => x.Id == 123);
        }

        [Fact]
        public void GetAlbumByID_MatchingAlbumInDB_ReturnsIt()
        {
            // Arrange
            var albumId = 1;
            var artistId = 1234;

            // We are currently required to care about an Artist ID because the convert part of album specifically references the artist repository as well.
            _context.Artists.Add(new Artist() { Id = artistId });
            _context.Albums.Add(new Album() { Id = 1, ArtistId = 1234 });
            _context.SaveChanges();

            // Act
            var album = _super.GetAlbumById(albumId);

            // Assert
            album.Id.Should().Be(1);
        }
    }
}