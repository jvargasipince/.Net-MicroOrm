using DataLayer.Entities;
using System;
using System.Collections.Generic;
using Poco = PetaPoco;

namespace DataLayer.PetaPoco
{
    [Poco.TableName("invoice")]
    [Poco.PrimaryKey("id")]
    public class InvoiceDecorated
    {
        public int id { get; set; }
        public string nroinvoice { get; set; }
        public string company { get; set; }
        public string customer { get; set; }
        public decimal ammount { get; set; }
        public int nroproducts { get; set; }
        public DateTime datecreate { get; set; }

        [Poco.Ignore]
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
