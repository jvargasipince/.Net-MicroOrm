using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Massive;

namespace DataLayer.Massive
{
    public class InvoiceRepository
    {

        //Complete Dynamic            

        public dynamic Find(int id)
        {
            dynamic table = new Invoices();
            return table.GetInvoice(Id: id);

            //var table = new Invoices();
            //return table.Single(key: id);

        }

        public List<dynamic> GetAllInvoicesAndDetails()
        {
            var table = new Invoices();

            string query = "select i.id, i.nroinvoice, i.company, i.customer, i.datecreate,";
            query += "id.id IdDetail, id.productname, id.quantity, id.subtotal, id.unitprice ";
            query += "from invoice i inner join invoiceDetail id on i.id = id.idInvoice";


            return table.Query(query).ToList();
        }

        public List<dynamic> GetAllInvoices()
        {

            var invoices = new DynamicModel("invoicesDB", "invoice", "id");
            return invoices.All().ToList();

            //dynamic invoices = new Invoices();
            //return (invoices.find() as IEnumerable<dynamic>).ToList();

        }

        public List<dynamic> GetAllInvoicesDetails()
        {
            var invoicesDetails = new DynamicModel("invoicesDB", "invoiceDetail", "id");
            return invoicesDetails.All().ToList();
        }

        public List<dynamic> GetAllInvoicesDetailsById(int id)
        {
            var invoicesDetails = new DynamicModel("invoicesDB", "invoiceDetail", "id");

            return invoicesDetails.All(where: "WHERE idInvoice=@0", args: id).ToList();
        }

        public dynamic AddInvoice(dynamic invoice)
        {
            var invoices = new DynamicModel("invoicesDB", "invoice", "id");          

            var addedInvoice = invoices.Insert(new {
                                                    nroinvoice = invoice.nroinvoice,
                                                    company = invoice.company,
                                                    customer = invoice.customer
                                                });

            return addedInvoice;
        }

        public dynamic AddDetails(dynamic invoiceDetail)
        {
            var invoicesDetail = new DynamicModel("invoicesDB", "invoiceDetail", "id");
            var addedInvoice = invoicesDetail.Insert(new
                                                    {
                                                        idInvoice = invoiceDetail.idInvoice,
                                                        productname = invoiceDetail.productname,
                                                        quantity = invoiceDetail.quantity,
                                                        unitprice = invoiceDetail.unitprice,
                                                        subtotal = invoiceDetail.subtotal
                                                    });

            return addedInvoice;
        }

        public dynamic Save(dynamic invoice)
        {
            using (var txScope = new TransactionScope())
            {
               var inserted = this.AddInvoice(invoice);

                foreach (var detail in invoice.InvoiceDetails)
                {
                    detail.idInvoice = inserted.id;
                    this.AddDetails(detail);
                }

                txScope.Complete();

            }
            return true;
        }

        public dynamic Update(dynamic invoice)
        {
            var table = new Invoices();
            table.Update(invoice, invoice.Id);

            return invoice;
        }

        public void Remove(int id)
        {
            var invoicesDetail = new DynamicModel("invoicesDB", "invoiceDetail", "id");
            invoicesDetail.Delete(where: "WHERE idInvoice=@0", args: id);

            var invoices = new DynamicModel("invoicesDB", "invoice", "id");
            invoices.Delete(id);
        }

        public void RemoveDetail(int id)
        {
            var invoicesDetail = new DynamicModel("invoicesDB", "invoiceDetail", "id");
            invoicesDetail.Delete(id);
        }

        public dynamic GetAllPaged(int currentPage, int pageSize)
        {
            var table = new Invoices();
            return table.Paged(currentPage: currentPage, pageSize: pageSize);
        }


    }
}
