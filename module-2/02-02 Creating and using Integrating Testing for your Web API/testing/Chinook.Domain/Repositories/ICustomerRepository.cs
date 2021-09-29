using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<List<Customer>> GetBySupportRepId(int id);
        Task<Customer> Add(Customer newCustomer);
        Task<bool> Update(Customer customer);
        Task<bool> Delete(int id);
    }
}