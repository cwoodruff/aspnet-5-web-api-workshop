﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.Repositories
{
    public interface IInvoiceRepository : IDisposable
    {
        Task<List<Invoice>> GetAll();
        Task<Invoice> GetById(int id);
        Task<List<Invoice>> GetByCustomerId(int id);
        Task<Invoice> Add(Invoice newInvoice);
        Task<bool> Update(Invoice invoice);
        Task<bool> Delete(int id);
        Task<List<Invoice>> GetByEmployeeId(int id);
    }
}