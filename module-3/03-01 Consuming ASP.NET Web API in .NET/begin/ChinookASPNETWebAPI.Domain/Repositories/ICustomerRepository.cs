using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.Repositories
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