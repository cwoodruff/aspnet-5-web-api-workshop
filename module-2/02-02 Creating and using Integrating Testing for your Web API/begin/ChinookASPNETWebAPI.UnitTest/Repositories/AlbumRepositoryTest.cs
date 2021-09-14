using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Repositories;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Repositories
{
    public class AlbumRepositoryTest
    {
        private readonly IAlbumRepository _repo;

        public AlbumRepositoryTest(IAlbumRepository a) => _repo = a;

        [Fact]
        public async Task AlbumGetAll()
        {
            // Arrange

            // Act
            var albums = await _repo.GetAll();

            // Assert
            Assert.True(albums.Count > 1, "The number of albums was not greater than 1");
        }

        [Fact]
        public void AlbumGetOne()
        {
            // Arrange
            var id = 1;

            // Act
            var album = _repo.GetById(id);

            // Assert
            Assert.Equal(id, album.Id);
        }
    }
}