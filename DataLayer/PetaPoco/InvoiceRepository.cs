using DataLayer.Entities;
using DataLayer.Repository;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace DataLayer.PetaPoco
{
    public class InvoiceRepository : IInvoiceRepository
    {

        private Database db = new Database("invoicesDB");

        public Invoice Add(Invoice invoice)
        {
            //insert(tablename, pk, isIdentityPK, values)
            this.db.Insert("invoice", "id", invoice);
            return invoice;
        }

        public InvoiceDecorated Add(InvoiceDecorated invoice)
        {
            //This is not necesary put the table and primarykey because 
            //the class InvoiceDecorated had PocoDecoration
            this.db.Insert(invoice);
            return invoice;
        }

        public Invoice find(int id)
        {

            //Using Decorated
            var result_decorated = this.db.SingleOrDefault<InvoiceDecorated>("WHERE Id = @0", id);
            var result_decorated_two = this.db.SingleOrDefault<InvoiceDecorated>(id);

            //With Stored Procedure
            var result_SP = this.db.Execute(";EXEC GetFullInvoice @0", id);

            string query = "Select id,  nroinvoice, companyid, customer, ammount, nroproducts, datecreate from invoice ";
            query += "where Id = @Id ";

            return this.db.Query<Invoice>(query, id).SingleOrDefault();

        }

        public List<Invoice> GetAll()
        {

            string query = "Select id,  nroinvoice, company, customer, datecreate from dbo.invoice; " +
                  "Select id, idInvoice, productname, quantity, unitprice, subtotal from dbo.invoiceDetail; ";

            //Return yeald return to iterate and not get all in memory
            var method_1 = this.db.Query<Invoice, InvoiceDetail>(query);

            //Return list<T> POCO
            var method_2 = this.db.Fetch<Invoice, InvoiceDetail>(query);

            //Using Decorated PetaPoco
            var method_3 = this.db.Query<InvoiceDecorated>("");

            //Like Dapper
            using (var multipleResults = this.db.QueryMultiple(query))
            {
                var invoices = multipleResults.Read<Invoice>().ToList();
                var invoicedetails = multipleResults.Read<InvoiceDetail>().ToList();

                foreach (var invoice in invoices)
                {
                    invoice.InvoiceDetails.AddRange(invoicedetails.Where(x => x.idInvoice == invoice.id).ToList());
                }

                return invoices;

            }
        }

        public Invoice GetFullInvoice(int id)
        {
            string getInvoice = "Select id,  nroinvoice, company, customer, datecreate from dbo.invoice where id=@id";
            string getInvoiceDetails = "Select id, idInvoice, productname, quantity, unitprice, subtotal from dbo.invoiceDetail where idInvoice=@id";

            Invoice invoice = this.db.SingleOrDefault<Invoice>(getInvoice, id);

            InvoiceDecorated invoiceDecorate = this.db.SingleOrDefault<InvoiceDecorated>("where id=@id", id);

            List<InvoiceDetail> invoiceDetails = this.db.Query<InvoiceDetail>(getInvoiceDetails, id).ToList();

            invoice.InvoiceDetails.AddRange(invoiceDetails);

            return invoice;

        }

        public void Remove(int id)
        {
            //Delete(tablename, pk, values)

            this.db.Delete<InvoiceDecorated>(id);

            this.db.Delete("invoice", "id", null, id);

            this.db.Execute("DELETE FROM invoiceDetail where idInvoice = @id; DELETE FROM invoice where id = @id ", new { id });

            using (var txScope = new TransactionScope())
            {
                this.RemoveDetail(id);

                this.db.Delete<InvoiceDecorated>(id);

                //Other way
                //this.db.Delete("invoice", "id", null, id);

                txScope.Complete();
            }

        }

        public void Save(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Invoice Update(Invoice invoice)
        {
            this.db.Update("invoice", "id", invoice);
            return invoice;
        }

        public InvoiceDecorated Update(InvoiceDecorated invoice)
        {
            this.Update(invoice);
            return invoice;
        }

        #region Details

        public InvoiceDetail Add(InvoiceDetail invoicedetail)
        {
            var sql = "insert into invoiceDetail (idInvoice, productname, quantity, unitprice, subtotal) " +
                       "values (@idInvoice, @productname, @quantity, @unitprice, @subtotal); " +
                        "Select cast(scope_identity() as int)";

            var id = this.db.Query<int>(sql, invoicedetail).Single();
            invoicedetail.id = id;
            return invoicedetail;
        }

        public void RemoveDetail(int id)
        {
            this.db.Delete("invoiceDetail", "id", null, id);
        }

        #endregion
    }
}
