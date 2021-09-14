using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Repositories;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Repositories
{
    public class TrackRepositoryTest
    {
        private readonly ITrackRepository _repo;

        public TrackRepositoryTest(ITrackRepository t) => _repo = t;

        [Fact]
        public async Task TrackGetAll()
        {
            // Act
            var tracks = await _repo.GetAll();

            // Assert
            Assert.True(tracks.Count > 1, "The number of tracks was not greater than 1");
        }
    }
}