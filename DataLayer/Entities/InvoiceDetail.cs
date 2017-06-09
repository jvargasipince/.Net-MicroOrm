using System;

namespace DataLayer.Entities
{
    public class InvoiceDetail
    {
        int id { get; set; }
        int idInvoice { get; set; }
        string productname { get; set; }
        int quantity { get; set; }
        decimal unitprice { get; set; }
        decimal subtotal { get; set; }
        DateTime datecreate { get; set; }
        bool status { get; set; }

    }
}
