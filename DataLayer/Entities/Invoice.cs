using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class Invoice
    {

        public Invoice()
        {
            this.InvoiceDetails = new List<InvoiceDetail>();
        }

        public int id { get; set; }
        public string nroinvoice { get; set; }
        public string company { get; set; }
        public string customer { get; set; }
        public decimal ammount { get; set; }
        public int nroproducts { get; set; }
        public DateTime datecreate { get; set; }

        public List<InvoiceDetail> InvoiceDetails { get; private set; }

        public bool IsNew
        {
            get
            {
                return this.id == default(int);
            }
        }

    }
}
