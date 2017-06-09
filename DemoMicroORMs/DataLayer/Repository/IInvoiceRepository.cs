using DemoMicroORMs.DataLayer.Entities;
using System.Collections.Generic;

namespace DemoMicroORMs.DataLayer.Repository
{
    public interface IInvoiceRepository
    {

        Invoice find(int id);
        List<Invoice> GetAll();
        Invoice Add(Invoice invoice);
        Invoice Update(Invoice invoice);
        void Remove(Invoice invoice);

        Invoice GetFullInvoice(int id);

        void Save(Invoice invoice);
    }
}
