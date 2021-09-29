using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ChinookContext _context;

        public InvoiceRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> InvoiceExists(int id) =>
            await _context.Invoices.AnyAsync(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        // public List<Invoice> GetAll() =>
        //     _context.Invoices.ToListAsync();

        public async Task<List<Invoice>> GetAll()
        {
            var invoices = _context.Invoices;
            return await invoices.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<Invoice> GetById(int id) =>
            await _context.Invoices.FindAsync(id);

        public async Task<Invoice> Add(Invoice newInvoice)
        {
            await _context.Invoices.AddAsync(newInvoice);
            await _context.SaveChangesAsync();
            return newInvoice;
        }

        public async Task<bool> Update(Invoice invoice)
        {
            if (!await InvoiceExists(invoice.Id))
                return false;
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await InvoiceExists(id))
                return false;
            var toRemove = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Invoice>> GetByEmployeeId(int id) =>
            await _context.Customers.Where(a => a.SupportRepId == 5).SelectMany(t => t.Invoices).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Invoice>> GetByCustomerId(int id) =>
            await _context.Invoices.Where(i => i.CustomerId == id).AsNoTrackingWithIdentityResolution().ToListAsync();
    }
}