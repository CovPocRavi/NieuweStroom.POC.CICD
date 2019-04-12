using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NieuweStroom.POC.CICD.Models;

namespace NieuweStroom.POC.CICD.DAL
{
    public class InvoiceRepository : IInvoiceRepository, IDisposable
    {
        private InvoiceContext context;
        private bool disposed = false;

        public InvoiceRepository(InvoiceContext context)
        {
            this.context = context;
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            //return context.Invoice.ToList();
            throw new NotImplementedException();
        }

        public Invoice GetInvoiceId(Guid invoiceId)
        {
                //return context.Invoice.Find(invoiceId);
                throw new NotImplementedException();
        }

        public void InsertInvoice(Invoice invoice)
        {
                //context.Invoice.Add(invoice);
                throw new NotImplementedException();
        }

        public void DeleteInvoice(Guid invoiceId)
        {
            //Invoice invoice = context.Invoice.Find(invoiceId);
            throw new NotImplementedException();
        }

        public void UpdateStudent(Invoice invoice)
        {
            //context.Entry(invoice).State = EntityState.Modified;
            throw new NotImplementedException();
        }

        public void Save()
        {
                //context.SaveChanges();
                throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
