using DataLayer.Entities;
using DataLayer.Repository;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataLayer.Test
{
    [TestClass]
    public class InvoiceRepositoryTests
    {

        [TestMethod]
        public void Get_all_should_return_4_invoices()
        {
            //arrange
            IInvoiceRepository repository = CreateRepository();

            //act
            var invoices = repository.GetAll();

            //asserts
            invoices.Should().NotBeNull();
            invoices.Count.Should().Be(4);
        }

        static int id;

        [TestMethod]
        public void Insert_should_Save_New_Invoice()
        {
            //arrange
            IInvoiceRepository repository = CreateRepository();
            var invoice = new Invoice
            {
                nroinvoice = "I0005",
                companyid = 1,
                customer = "Jorge Vargas",
                ammount = 380.75m,
                nroproducts = 2,
                datecreate = DateTime.Now,
                status = true
            };

            //act
            repository.Add(invoice);

            //asserts
            invoice.id.Should().NotBe(0, "because the id will be assigned by identity");
            Console.WriteLine("New id: " + invoice.id);
            id = invoice.id;

        }

        [TestMethod]
        public void Find_should_return_existing_invoice()
        {
            //arrange
            IInvoiceRepository repository = CreateRepository();

            //act
            var invoice = repository.find(id);

            //asserts
            invoice.Should().NotBeNull();
            invoice.id.Should().Be(id);
            invoice.nroinvoice.Should().Be("I0005");
            invoice.companyid.Should().Be(1);
            invoice.customer.Should().Be("Jorge Vargas");
            invoice.ammount.Should().Be(380.75m);
            invoice.nroproducts.Should().Be(2);
            invoice.datecreate.Should().Be(DateTime.Now);
            invoice.status.Should().Be(true);
        }


        [TestMethod]
        public void Update_should_update_existing_invoice()
        {
            //arrange
            IInvoiceRepository repository = CreateRepository();

            //act
            var invoice = repository.find(id);
            invoice.customer = "Bill Gates";

            repository.Update(invoice);

            //create a new repository for verification purposes
            IInvoiceRepository repository2 = CreateRepository();
            var invoiceUpdate = repository2.find(id);

            //asserts
            invoiceUpdate.customer.Should().Be("Bill Gates");

        }

        [TestMethod]
        public void Delete_should_remove_invoice()
        {
            //arrange
            IInvoiceRepository repository = CreateRepository();

            //act
            repository.Remove(id);

            //create a new repository for verification purposes
            IInvoiceRepository repository2 = CreateRepository();
            var deleteInvoice = repository2.find(id);

            //asserts
            deleteInvoice.Should().BeNull();

        }


        #region StandarMethods


        private IInvoiceRepository CreateRepository()
        {
            return new Dapper.InvoiceRepository();
        }


        #endregion
    }
}
