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
    public class EmployeeSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly EmployeeRepository _employeeRepo;
        private readonly ChinookContext _context;

        public EmployeeSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _employeeRepo = new EmployeeRepository(_context);
            _super = new ChinookSupervisor(null, null, null, _employeeRepo, 
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
            _employeeRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task EmployeeGetAll()
        {
            // Act
            var employees = (await _super.GetAllEmployee()).ToList();

            // Assert
            Assert.True(employees.Count > 1, "The number of employees was not greater than 1");
        }
    }
}