using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly ChinookContext _context;

        public InvoiceLineRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> InvoiceLineExists(int id) =>
            await _context.InvoiceLines.AnyAsync(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<InvoiceLine>> GetAll() =>
            await _context.InvoiceLines.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<InvoiceLine> GetById(int id) =>
            await _context.InvoiceLines.FindAsync(id);

        public async Task<InvoiceLine> Add(InvoiceLine newInvoiceLine)
        {
            await _context.InvoiceLines.AddAsync(newInvoiceLine);
            await _context.SaveChangesAsync();
            return newInvoiceLine;
        }

        public async Task<bool> Update(InvoiceLine invoiceLine)
        {
            if (!await InvoiceLineExists(invoiceLine.Id))
                return false;
            _context.InvoiceLines.Update(invoiceLine);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await InvoiceLineExists(id))
                return false;
            var toRemove = await _context.InvoiceLines.FindAsync(id);
            _context.InvoiceLines.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<InvoiceLine>> GetByInvoiceId(int id) =>
            await _context.InvoiceLines.Where(a => a.InvoiceId == id).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<InvoiceLine>> GetByTrackId(int id) =>
            await _context.InvoiceLines.Where(a => a.TrackId == id).AsNoTrackingWithIdentityResolution().ToListAsync();
    }
}