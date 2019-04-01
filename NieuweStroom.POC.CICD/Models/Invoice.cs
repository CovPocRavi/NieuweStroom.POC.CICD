using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NieuweStroom.POC.CICD.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid? MeterId { get; set; }

        public int InvoiceNumber { get; set; }

        public DateTimeOffset? InvoiceDate { get; set; }

        public DateTimeOffset IntervalStart { get; set; }

        public DateTimeOffset IntervalEnd { get; set; }

        public double AmountExcVat { get; set; }

        public double AmountVat { get; set; }

        public double AmountIncVat { get; set; }

        public string Description { get; set; }

        public int InvoiceTypeId { get; set; }

        public int PublicUtilityId { get; set; }

        public Guid? DocumentId { get; set; }
    }
}
