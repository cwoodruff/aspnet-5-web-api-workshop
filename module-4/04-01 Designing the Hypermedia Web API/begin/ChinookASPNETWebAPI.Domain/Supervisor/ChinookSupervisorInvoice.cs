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
        public async Task<IEnumerable<InvoiceApiModel>> GetAllInvoice()
        {
            List<Invoice> invoices = await _invoiceRepository.GetAll();
            var invoiceApiModels = invoices.ConvertAll();

            foreach (var invoice in invoiceApiModels)
            {
                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("Invoice-", invoice.Id), invoice, (TimeSpan)cacheEntryOptions);
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
                var invoice = await _invoiceRepository.GetById(id);
                if (invoice == null) return null;
                var invoiceApiModel = invoice.Convert();
                invoiceApiModel.InvoiceLines = (await GetInvoiceLineByInvoiceId(invoiceApiModel.Id)).ToList();

                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("Invoice-", invoiceApiModel.Id), invoiceApiModel, (TimeSpan)cacheEntryOptions);

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

            var invoice = newInvoiceApiModel.Convert();

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