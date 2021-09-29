using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<InvoiceApiModel>> GetAllInvoice()
        {
            List<Invoice> invoices = await _invoiceRepository.GetAll();
            var invoiceApiModels = invoices.ConvertAll();
            foreach (var invoice in invoiceApiModels)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Invoice-", invoice.Id), invoice, cacheEntryOptions);
            }

            return invoiceApiModels;
        }

        public async Task<InvoiceApiModel> GetInvoiceById(int id)
        {
            var invoiceApiModelCached = _cache.Get<InvoiceApiModel>(string.Concat("Invoice-", id));

            if (invoiceApiModelCached != null)
            {
                return invoiceApiModelCached;
            }
            else
            {
                var invoiceApiModel = await (await _invoiceRepository.GetById(id)).ConvertAsync();
                invoiceApiModel.Customer = await GetCustomerById(invoiceApiModel.CustomerId);
                invoiceApiModel.InvoiceLines = (await GetInvoiceLineByInvoiceId(invoiceApiModel.Id)).ToList();
                invoiceApiModel.CustomerName =
                    $"{invoiceApiModel.Customer.LastName}, {invoiceApiModel.Customer.FirstName}";

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Invoice-", invoiceApiModel.Id), invoiceApiModel, cacheEntryOptions);

                return invoiceApiModel;
            }
        }

        public async Task<IEnumerable<InvoiceApiModel>> GetInvoiceByCustomerId(int id)
        {
            var invoices = await _invoiceRepository.GetByCustomerId(id);
            return invoices.ConvertAll();
        }

        public async Task<InvoiceApiModel> AddInvoice(InvoiceApiModel newInvoiceApiModel)
        {
            await _invoiceValidator.ValidateAndThrowAsync(newInvoiceApiModel);
            var invoice = await newInvoiceApiModel.ConvertAsync();

            invoice = await _invoiceRepository.Add(invoice);
            newInvoiceApiModel.Id = invoice.Id;
            return newInvoiceApiModel;
        }

        public async Task<bool> UpdateInvoice(InvoiceApiModel invoiceApiModel)
        {
            await _invoiceValidator.ValidateAndThrowAsync(invoiceApiModel);
            var invoice = await _invoiceRepository.GetById(invoiceApiModel.Id);

            if (invoice == null) return false;
            invoice.Id = invoiceApiModel.Id;
            invoice.CustomerId = invoiceApiModel.CustomerId;
            invoice.InvoiceDate = invoiceApiModel.InvoiceDate;
            invoice.BillingAddress = invoiceApiModel.BillingAddress ?? string.Empty;
            invoice.BillingCity = invoiceApiModel.BillingCity ?? string.Empty;
            invoice.BillingState = invoiceApiModel.BillingState ?? string.Empty;
            invoice.BillingCountry = invoiceApiModel.BillingCountry ?? string.Empty;
            invoice.BillingPostalCode = invoiceApiModel.BillingPostalCode ?? string.Empty;
            invoice.Total = invoiceApiModel.Total;

            return await _invoiceRepository.Update(invoice);
        }

        public Task<bool> DeleteInvoice(int id)
            => _invoiceRepository.Delete(id);


        public async Task<IEnumerable<InvoiceApiModel>> GetInvoiceByEmployeeId(int id)
        {
            var invoices = await _invoiceRepository.GetByEmployeeId(id);
            return invoices.ConvertAll();
        }
    }
}