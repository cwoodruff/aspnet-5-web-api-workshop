using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Entities;
using ChinookASPNETWebAPI.Domain.Extensions;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace ChinookASPNETWebAPI.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<CustomerApiModel>> GetAllCustomer()
        {
            List<Customer> customers = await _customerRepository.GetAll();
            var customerApiModels = customers.ConvertAll();

            foreach (var customer in customerApiModels)
            {
                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("Customer-", customer.Id), customer, (TimeSpan)cacheEntryOptions);
            }

            return customerApiModels;
        }

        public async Task<CustomerApiModel> GetCustomerById(int id)
        {
            var customerApiModelCached = _cache.Get<CustomerApiModel>(string.Concat("Customer-", id));

            if (customerApiModelCached != null)
            {
                return customerApiModelCached;
            }
            else
            {
                var customer = await _customerRepository.GetById(id);
                if (customer == null) return null;
                var customerApiModel = customer.Convert();
                customerApiModel.Invoices = (await GetInvoiceByCustomerId(customerApiModel.Id)).ToList();
                customerApiModel.SupportRep =
                    await GetEmployeeById(customerApiModel.SupportRepId);
                customerApiModel.SupportRepName =
                    $"{customerApiModel.SupportRep.LastName}, {customerApiModel.SupportRep.FirstName}";

                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("Customer-", customerApiModel.Id), customerApiModel, (TimeSpan)cacheEntryOptions);

                return customerApiModel;
            }
        }

        public async Task<IEnumerable<CustomerApiModel>> GetCustomerBySupportRepId(int id)
        {
            var customers = await _customerRepository.GetBySupportRepId(id);

            foreach (var customer in customers)
            {
                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("Customer-", customer.Id), customer, (TimeSpan)cacheEntryOptions);
            }

            return customers.ConvertAll();
        }

        public async Task<CustomerApiModel> AddCustomer(CustomerApiModel newCustomerApiModel)
        {
            await _customerValidator.ValidateAndThrowAsync(newCustomerApiModel);

            var customer = newCustomerApiModel.Convert();

            customer = await _customerRepository.Add(customer);
            newCustomerApiModel.Id = customer.Id;
            return newCustomerApiModel;
        }

        public async Task<bool> UpdateCustomer(CustomerApiModel customerApiModel)
        {
            await _customerValidator.ValidateAndThrowAsync(customerApiModel);

            var customer = await _customerRepository.GetById(customerApiModel.Id);

            if (customer == null) return false;
            customer.FirstName = customerApiModel.FirstName;
            customer.LastName = customerApiModel.LastName;
            customer.Company = customerApiModel.Company ?? string.Empty;
            customer.Address = customerApiModel.Address ?? string.Empty;
            customer.City = customerApiModel.City ?? string.Empty;
            customer.State = customerApiModel.State ?? string.Empty;
            customer.Country = customerApiModel.Country ?? string.Empty;
            customer.PostalCode = customerApiModel.PostalCode ?? string.Empty;
            customer.Phone = customerApiModel.Phone ?? string.Empty;
            customer.Fax = customerApiModel.Fax ?? string.Empty;
            customer.Email = customerApiModel.Email ?? string.Empty;
            customer.SupportRepId = customerApiModel.SupportRepId;

            return await _customerRepository.Update(customer);
        }

        public Task<bool> DeleteCustomer(int id)
            => _customerRepository.Delete(id);
    }
}