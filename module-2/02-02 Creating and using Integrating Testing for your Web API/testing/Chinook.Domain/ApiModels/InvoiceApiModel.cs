using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class InvoiceApiModel : IConvertModel<InvoiceApiModel, Invoice>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore] public IList<InvoiceLineApiModel> InvoiceLines { get; set; }

        [JsonIgnore] public CustomerApiModel Customer { get; set; }

        public Invoice Convert() =>
            new()
            {
                Id = Id,
                CustomerId = CustomerId,
                InvoiceDate = InvoiceDate,
                BillingAddress = BillingAddress,
                BillingCity = BillingCity,
                BillingState = BillingState,
                BillingCountry = BillingCountry,
                BillingPostalCode = BillingPostalCode,
                Total = Total
            };

        public async Task<Invoice> ConvertAsync() =>
            new()
            {
                Id = Id,
                CustomerId = CustomerId,
                InvoiceDate = InvoiceDate,
                BillingAddress = BillingAddress,
                BillingCity = BillingCity,
                BillingState = BillingState,
                BillingCountry = BillingCountry,
                BillingPostalCode = BillingPostalCode,
                Total = Total
            };
    }
}