using System;

namespace DemoMicroORMs.DataLayer.Entities
{
    public class Invoice
    {

        int id { get; set; }
        string nroinvoice { get; set; }
        int companyid { get; set; }
        string customer { get; set; }
        decimal ammount { get; set; }
        int nroproducts { get; set; }
        DateTime datecreate { get; set; }
        bool status { get; set; }


    }
}
