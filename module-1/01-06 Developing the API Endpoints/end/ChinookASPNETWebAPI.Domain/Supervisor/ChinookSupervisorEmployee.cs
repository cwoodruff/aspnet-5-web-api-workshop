using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Entities;
using ChinookASPNETWebAPI.Domain.Extensions;

namespace ChinookASPNETWebAPI.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<EmployeeApiModel>> GetAllEmployee()
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            var employeeApiModels = employees.ConvertAll();

            return employeeApiModels;
        }

        public async Task<EmployeeApiModel> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee == null) return null;
            var employeeApiModel = employee.Convert();

            return employeeApiModel;
        }

        public async Task<EmployeeApiModel> GetEmployeeReportsTo(int id)
        {
            var employee = await _employeeRepository.GetReportsTo(id);
            return employee.Convert();
        }

        public async Task<EmployeeApiModel> AddEmployee(EmployeeApiModel newEmployeeApiModel)
        {
            var employee = newEmployeeApiModel.Convert();

            employee = await _employeeRepository.Add(employee);
            newEmployeeApiModel.Id = employee.Id;
            return newEmployeeApiModel;
        }

        public async Task<bool> UpdateEmployee(EmployeeApiModel employeeApiModel)
        {
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