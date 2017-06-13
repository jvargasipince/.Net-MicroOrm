using System.Collections.Generic;
using DataLayer.Entities;

namespace DataLayer.Repository
{
    public interface IInvoiceRepository
    {
        Invoice find(int id);
        List<Invoice> GetAll();
        Invoice Add(Invoice invoice);
        Invoice Update(Invoice invoice);
        void Remove(int id);
        Invoice GetFullInvoice(int id);
        void Save(Invoice invoice);
        void RemoveDetail(int id);
    }
}
