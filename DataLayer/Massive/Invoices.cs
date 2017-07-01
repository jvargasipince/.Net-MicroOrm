using Massive;

namespace DataLayer.Massive
{
    public class Invoices : DynamicModel
    {

        public Invoices()
        : base("invoicesDB", "id")
            {

            }

        //public Invoices()
        //    : base("invoicesDB", "invoice", "id")
        //{

        //}
    }
}
