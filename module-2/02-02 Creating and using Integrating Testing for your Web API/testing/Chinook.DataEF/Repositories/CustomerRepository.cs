using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ChinookContext _context;

        public CustomerRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> CustomerExists(int id) =>
            await _context.Customers.AnyAsync(c => c.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<Customer>> GetAll() =>
            await _context.Customers.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Customer> GetById(int id) =>
            await _context.Customers.FindAsync(id);

        public async Task<Customer> Add(Customer newCustomer)
        {
            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<bool> Update(Customer customer)
        {
            if (!await CustomerExists(customer.Id))
                return false;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await CustomerExists(id))
                return false;
            var toRemove = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Customer>> GetBySupportRepId(int id) =>
            await _context.Customers.Where(a => a.SupportRepId == id).AsNoTrackingWithIdentityResolution().ToListAsync();
    }
}