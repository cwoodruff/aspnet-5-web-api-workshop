﻿#nullable disable

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public partial class InvoiceLine
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Track Track { get; set; }
    }
}
