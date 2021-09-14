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
    public class InvoiceSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly InvoiceRepository _invoiceRepo;
        private readonly ChinookContext _context;

        public InvoiceSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _invoiceRepo = new InvoiceRepository(_context);
            _super = new ChinookSupervisor(null, null, null, null, 
                null, null, _invoiceRepo, null, 
                null, null, null, null,
                null, null, null, null, null,
                null, null, null, new MemoryCache(new MemoryCacheOptions()), 
                null);
        }
        
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _invoiceRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task InvoiceGetAll()
        {
            // Act
            var invoices = (await _super.GetAllInvoice()).ToList();

            // Assert
            Assert.True(invoices.Count > 1, "The number of invoices was not greater than 1");
        }
    }
}