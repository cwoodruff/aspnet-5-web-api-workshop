using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Repositories;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Repositories
{
    public class GenreRepositoryTest
    {
        private readonly IGenreRepository _repo;

        public GenreRepositoryTest(IGenreRepository g) => _repo = g;

        [Fact]
        public async Task GenreGetAll()
        {
            // Act
            var genres = await _repo.GetAll();

            // Assert
            Assert.True(genres.Count > 1, "The number of genres was not greater than 1");
        }
    }
}