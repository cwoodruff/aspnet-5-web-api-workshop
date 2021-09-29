using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class EmployeeApiModel : IConvertModel<EmployeeApiModel, Employee>
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Title { get; set; }
        public int? ReportsTo { get; set; }
        public string? ReportsToName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }

        [JsonIgnore] public List<CustomerApiModel>? Customers { get; set; }

        [JsonIgnore] public EmployeeApiModel? Manager { get; set; }

        [JsonIgnore] public ICollection<EmployeeApiModel>? DirectReports { get; set; }

        public Employee Convert() =>
            new()
            {
                Id = Id,
                LastName = LastName ?? string.Empty,
                FirstName = FirstName ?? string.Empty,
                Title = Title ?? string.Empty,
                ReportsTo = ReportsTo,
                BirthDate = BirthDate,
                HireDate = HireDate,
                Address = Address ?? string.Empty,
                City = City ?? string.Empty,
                State = State ?? string.Empty,
                Country = Country ?? string.Empty,
                PostalCode = PostalCode ?? string.Empty,
                Phone = Phone ?? string.Empty,
                Fax = Fax ?? string.Empty,
                Email = Email ?? string.Empty
            };

        public async Task<Employee> ConvertAsync() =>
            new()
            {
                Id = Id,
                LastName = LastName ?? string.Empty,
                FirstName = FirstName ?? string.Empty,
                Title = Title ?? string.Empty,
                ReportsTo = ReportsTo,
                BirthDate = BirthDate,
                HireDate = HireDate,
                Address = Address ?? string.Empty,
                City = City ?? string.Empty,
                State = State ?? string.Empty,
                Country = Country ?? string.Empty,
                PostalCode = PostalCode ?? string.Empty,
                Phone = Phone ?? string.Empty,
                Fax = Fax ?? string.Empty,
                Email = Email ?? string.Empty
            };
    }
}