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
    public class CustomerSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly CustomerRepository _customerRepo;
        private readonly ChinookContext _context;

        public CustomerSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _customerRepo = new CustomerRepository(_context);
            _super = new ChinookSupervisor(null, null, _customerRepo, null, 
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
            _customerRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task CustomerGetAll()
        {
            // Act
            var customers = (await _super.GetAllCustomer()).ToList();

            // Assert
            Assert.True(customers.Count > 1, "The number of customers was not greater than 1");
        }
    }
}