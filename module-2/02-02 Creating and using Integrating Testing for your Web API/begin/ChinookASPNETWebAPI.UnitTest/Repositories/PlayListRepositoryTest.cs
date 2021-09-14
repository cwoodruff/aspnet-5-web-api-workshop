using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Repositories;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Repositories
{
    public class PlayListRepositoryTest
    {
        private readonly IPlaylistRepository _repo;

        public PlayListRepositoryTest(IPlaylistRepository p) => _repo = p;

        [Fact]
        public async Task PlayListGetAll()
        {
            // Act
            var playLists = await _repo.GetAll();

            // Assert
            Assert.True(playLists.Count > 1, "The number of play lists was not greater than 1");
        }
    }
}