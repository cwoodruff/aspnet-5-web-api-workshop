using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Repositories;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Repositories
{
    public class EmployeeRepositoryTest
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeRepositoryTest(IEmployeeRepository e) => _repo = e;

        [Fact]
        public async Task EmployeeGetAll()
        {
            // Act
            var employees = await _repo.GetAll();

            // Assert
            Assert.True(employees.Count > 1, "The number of employees was not greater than 1");
        }
    }
}