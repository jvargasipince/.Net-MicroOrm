using DemoMicroORMs.DataLayer.Entities;
using System.Collections.Generic;

namespace DemoMicroORMs.DataLayer.Repository
{
    public interface IInvoiceDetailRepository
    {
        InvoiceDetail find(int id);

        InvoiceDetail getByInvoiceId(int id);
        List<InvoiceDetail> GetAll();
        InvoiceDetail Add(InvoiceDetail invoice);
        InvoiceDetail Update(InvoiceDetail invoice);
        void Remove(InvoiceDetail invoice);

        void Save(InvoiceDetail invoice);
    }
}
