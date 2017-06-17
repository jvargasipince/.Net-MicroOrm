using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;
using DataLayer.Entities;
using DataLayer.Repository;

namespace DataLayer.Dapper
{
    public class InvoiceRepository : IInvoiceRepository
    {

        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["invoicesDB"].ConnectionString);

        public Invoice find(int id)
        {
            string query = "Select id,  nroinvoice, companyid, customer, ammount, nroproducts, datecreate from invoice ";
            query += "where Id = @Id ";
            return this.db.Query<Invoice>(query, new { Id = id }).SingleOrDefault();
        }

        public Invoice Add(Invoice invoice)
        {
            //using dinamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", value: invoice.id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@nroinvoice", value: invoice.nroinvoice);
            parameters.Add("@company", value: invoice.company);
            parameters.Add("@customer", value: invoice.customer);
            parameters.Add("@ammount", value: invoice.ammount, dbType: DbType.Decimal);
            parameters.Add("@nroproducts", value: invoice.nroproducts, dbType: DbType.Int32);

            var id = this.db.Execute("InsertInvoice", parameters, commandType: CommandType.StoredProcedure);
            invoice.id = parameters.Get<int>("@id");
            return invoice;
        }


        public List<Invoice> GetAll()
        {
            string query = "Select id, nroinvoice, company, customer, ammount, nroproducts, datecreate from invoice";
            return this.db.Query<Invoice>(query).ToList();
        }

        public Invoice GetFullInvoice(int id)
        {
            using (var multipleResults = this.db.QueryMultiple("GetFullInvoice", new { id }, commandType: CommandType.StoredProcedure))
            {
                var invoice = multipleResults.Read<Invoice>().SingleOrDefault();
                var invoicedetails = multipleResults.Read<InvoiceDetail>().ToList();

                if (invoice != null && invoicedetails != null)
                {
                    invoice.InvoiceDetails.AddRange(invoicedetails);
                }

                return invoice;

            }

        }

        public void Remove(int id)
        {
            this.db.Execute("DELETE FROM invoiceDetail where idInvoice = @id; DELETE FROM invoice where id = @id ", new { id });
        }



        public void Save(Invoice invoice)
        {
            using (var txScope = new TransactionScope())
            {
                this.Add(invoice);                

                foreach (var detail in invoice.InvoiceDetails)
                {
                    detail.idInvoice = invoice.id;
                    this.Add(detail);
                }

                txScope.Complete();

            }
        }

        public Invoice Update(Invoice invoice)
        {
            var sql = " UPDATE invoice " +
                "SET    nroinvoice = @nroinvoice, " +
                "       companyid = @nroinvoice, " +
                "       customer = @nroinvoice, " +
                "       ammount = @ammount, " +
                "       nroproducts = @nroproducts " +
                "WHERE id = @id ";

            this.db.Execute(sql, invoice);

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
            this.db.Execute("DELETE FROM invoiceDetail where id = @id", new { id });
        }

        #endregion

    }
}
