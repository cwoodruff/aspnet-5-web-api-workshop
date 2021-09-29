using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<EmployeeApiModel>> GetAllEmployee()
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            var employeeApiModels = employees.ConvertAll();
            foreach (var employee in employeeApiModels)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Employee-", employee.Id), employee, cacheEntryOptions);
            }

            return employeeApiModels;
        }

        public async Task<EmployeeApiModel> GetEmployeeById(int id)
        {
            var employeeApiModelCached = _cache.Get<EmployeeApiModel>(string.Concat("Employee-", id));

            if (employeeApiModelCached != null)
            {
                return employeeApiModelCached;
            }
            else
            {
                var employeeApiModel = await (await _employeeRepository.GetById(id)).ConvertAsync();
                //employeeApiModel.Customers = (GetCustomerBySupportRepId(employeeApiModel.Id)).ToList();
                //employeeApiModel.DirectReports = (GetEmployeeDirectReports(employeeApiModel.EmployeeId)).ToList();
                // employeeApiModel.Manager = employeeApiModel.ReportsTo.HasValue
                //     ? GetEmployeeReportsTo(id)
                //     : null;
                // if (employeeApiModel.Manager != null)
                //     employeeApiModel.ReportsToName = employeeApiModel.ReportsTo.HasValue
                //         ? $"{employeeApiModel.Manager.LastName}, {employeeApiModel.Manager.FirstName}"
                //         : string.Empty;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Employee-", employeeApiModel.Id), employeeApiModel,
                    cacheEntryOptions);

                return employeeApiModel;
            }
        }

        public async Task<EmployeeApiModel> GetEmployeeReportsTo(int id)
        {
            var employee = await _employeeRepository.GetReportsTo(id);
            return await employee.ConvertAsync();
        }

        public async Task<EmployeeApiModel> AddEmployee(EmployeeApiModel newEmployeeApiModel)
        {
            await _employeeValidator.ValidateAndThrowAsync(newEmployeeApiModel);
            var employee = await newEmployeeApiModel.ConvertAsync();

            employee = await _employeeRepository.Add(employee);
            newEmployeeApiModel.Id = employee.Id;
            return newEmployeeApiModel;
        }

        public async Task<bool> UpdateEmployee(EmployeeApiModel employeeApiModel)
        {
            await _employeeValidator.ValidateAndThrowAsync(employeeApiModel);
            var employee = await _employeeRepository.GetById(employeeApiModel.Id);

            if (employee == null) return false;
            employee.Id = employeeApiModel.Id;
            employee.LastName = employeeApiModel.LastName;
            employee.FirstName = employeeApiModel.FirstName;
            employee.Title = employeeApiModel.Title ?? string.Empty;
            employee.ReportsTo = employeeApiModel.ReportsTo;
            employee.BirthDate = employeeApiModel.BirthDate;
            employee.HireDate = employeeApiModel.HireDate;
            employee.Address = employeeApiModel.Address ?? string.Empty;
            employee.City = employeeApiModel.City ?? string.Empty;
            employee.State = employeeApiModel.State ?? string.Empty;
            employee.Country = employeeApiModel.Country ?? string.Empty;
            employee.PostalCode = employeeApiModel.PostalCode ?? string.Empty;
            employee.Phone = employeeApiModel.Phone ?? string.Empty;
            employee.Fax = employeeApiModel.Fax ?? string.Empty;
            employee.Email = employeeApiModel.Email ?? string.Empty;

            return await _employeeRepository.Update(employee);
        }

        public Task<bool> DeleteEmployee(int id)
            => _employeeRepository.Delete(id);

        public async Task<IEnumerable<EmployeeApiModel>> GetEmployeeDirectReports(int id)
        {
            var employees = await _employeeRepository.GetDirectReports(id);
            return employees.ConvertAll();
        }

        public async Task<IEnumerable<EmployeeApiModel>> GetDirectReports(int id)
        {
            var employees = await _employeeRepository.GetDirectReports(id);
            return employees.ConvertAll();
        }
    }
}