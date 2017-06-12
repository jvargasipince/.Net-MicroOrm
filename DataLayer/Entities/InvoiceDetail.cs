namespace DataLayer.Entities
{
    public class InvoiceDetail
    {
        public int id { get; set; }
        public int idInvoice { get; set; }
        public string productname { get; set; }
        public int quantity { get; set; }
        public decimal unitprice { get; set; }
        public decimal subtotal { get; set; }
        public bool IsNew
        {
            get
            {
                return this.id == default(int);
            }
        }

        public bool IsDeleted { get; set; }


    }
}
