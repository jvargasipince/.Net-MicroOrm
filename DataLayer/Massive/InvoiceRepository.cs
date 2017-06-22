using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Massive
{
    public class InvoiceRepository
    {

        //Complete Dynamic            


        public dynamic Find(int id)
        {
            dynamic table = new Invoices();
            return table.GetContact(Id: id);

            //var table = new Invoices();
            //return table.Single(key: id);

        }

        public List<dynamic> GetAll()
        {
            //var table = new DynamicModel("invoicesDB", "invoice", "id");
            //return table.All().ToList();

            //dynamic table = new Invoices();
            //return (table.find() as IEnumerable<dynamic>).ToList();

            var table = new Invoices();
            return table.Query("select id, nroinvoice, company, customer, datecreate from invoice").ToList();

        }
        public dynamic Add(dynamic contact)
        {
            var table = new Invoices();
            var addedInvoice = table.Insert(contact);

            return addedInvoice;
        }

        public dynamic Update(dynamic contact)
        {
            var table = new Invoices();
            table.Update(contact, contact.Id);

            return contact;

        }
        public void Remove(int id)
        {
            var table = new Invoices();
            table.Delete(id);
        }

        public dynamic GetAllPaged(int currentPage, int pageSize)
        {
            var table = new Invoices();
            return table.Paged(currentPage: currentPage, pageSize: pageSize);
        }


    }
}
