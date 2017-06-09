using DemoMicroORMs.DataLayer.Repository;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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


        #region StandarMethods


        private IInvoiceRepository CreateRepository()
        {
            return new DemoMicroORMs.Dapper.InvoiceRepository();
        }


        #endregion
    }
}
