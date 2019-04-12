using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NieuweStroom.POC.CICD.Models;

namespace NieuweStroom.POC.CICD.DAL
{
   public interface IInvoiceRepository : IDisposable
   {
       IEnumerable<Invoice> GetInvoices();
       Invoice GetInvoiceId(Guid invoiceId);
       void InsertInvoice(Invoice invoice);
       void DeleteInvoice(Guid invoiceId);
       void UpdateStudent(Invoice invoice);
       void Save();
   }
}
