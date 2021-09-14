using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Repositories;
using Xunit;

namespace ChinookASPNETWebAPI.UnitTest.Repositories
{
    public class InvoiceLineRepositoryTest
    {
        private readonly IInvoiceLineRepository _repo;

        public InvoiceLineRepositoryTest(IInvoiceLineRepository i) => _repo = i;

        [Fact]
        public async Task InvoiceLineGetAll()
        {
            // Act
            var invoiceLines = await _repo.GetAll();

            // Assert
            Assert.True(invoiceLines.Count > 1, "The number of invoice lines was not greater than 1");
        }
    }
}