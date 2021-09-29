using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ChinookContext _context;

        public EmployeeRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> EmployeeExists(int id) =>
            await _context.Employees.AnyAsync(e => e.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<Employee>> GetAll() =>
            await _context.Employees.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Employee> GetById(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task<Employee> Add(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return newEmployee;
        }

        public async Task<bool> Update(Employee employee)
        {
            if (!await EmployeeExists(employee.Id))
                return false;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await EmployeeExists(id))
                return false;
            var toRemove = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> GetReportsTo(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task<List<Employee>> GetDirectReports(int id) =>
            await _context.Employees.Where(e => e.ReportsTo == id).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Employee> GetToReports(int id) =>
            await _context.Employees
                .FindAsync(_context.Employees.Where(e => e.Id == id)
                    .Select(p => new { p.ReportsTo })
                    .First());
    }
}