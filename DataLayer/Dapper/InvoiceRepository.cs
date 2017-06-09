using Dapper;
using DataLayer.Entities;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.Dapper
{
    public class InvoiceRepository : IInvoiceRepository
    {

        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["invoicesDB"].ConnectionString);

        public Invoice find(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice Add(Invoice invoice)
        {
            var sql = "insert into invoice (nroinvoice, companyid, customer, ammount, nroproducts) " +
                       "values (@nroinvoice, @companyid, @customer, @ammount, @nroproducts); " +
                        "Select cast(scope_identity() as int)";

            var id = this.db.Query<int>(sql, invoice).Single();
            invoice.id = id;
            return invoice;
        }


        public List<Invoice> GetAll()
        {
            return this.db.Query<Invoice>("Select id,  nroinvoice, companyid, customer, ammount, nroproducts, datecreate, status from invoice").ToList();
        }

        public Invoice GetFullInvoice(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Invoice Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
