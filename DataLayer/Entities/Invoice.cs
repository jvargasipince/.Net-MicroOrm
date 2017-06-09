using System;

namespace DataLayer.Entities
{
    public class Invoice
    {

        public int id { get; set; }
        public string nroinvoice { get; set; }
        public int companyid { get; set; }
        public string customer { get; set; }
        public decimal ammount { get; set; }
        public int nroproducts { get; set; }
        public DateTime datecreate { get; set; }
        public bool status { get; set; }

    }
}
