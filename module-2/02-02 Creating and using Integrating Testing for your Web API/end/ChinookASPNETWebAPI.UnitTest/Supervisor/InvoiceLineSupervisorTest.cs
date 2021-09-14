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
    public class InvoiceLineSupervisorTest : IDisposable
    {
        private readonly IChinookSupervisor _super;
        private readonly InvoiceLineRepository _invoiceLineRepo;
        private readonly ChinookContext _context;

        public InvoiceLineSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _invoiceLineRepo = new InvoiceLineRepository(_context);
            _super = new ChinookSupervisor(null, null, null, null, 
                null, _invoiceLineRepo, null, null, 
                null, null, null, null,
                null, null, null, null, null,
                null, null, null, new MemoryCache(new MemoryCacheOptions()), 
                null);
        }
        
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _invoiceLineRepo?.Dispose();
            _context?.Dispose();
        }

        [Fact]
        public async Task InvoiceLineGetAll()
        {
            // Act
            var invoiceLines = (await _super.GetAllInvoiceLine()).ToList();

            // Assert
            Assert.True(invoiceLines.Count > 1, "The number of invoice lines was not greater than 1");
        }
    }
}