using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSampleA16
{
    public class address
    {
        public string line1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
    }
    public class shipto
    {
        public address address { get; set; }
    }
    public class defaultLocations
    {
       // public shipto ShipTo { get; set; }
       // public shipto ShipFrom { get; set; }
        public shipto POS { get; set; }
    }
    public class header
    {
        public string accountId { get; set; }
        public string transactionType { get; set; }
        public string companyCode { get; set; }
        public string accountingEntityCode { get; set; }
        public string documentCode { get; set; }
        public string customerCode { get; set; }
        public string transactionDate { get; set; }
        public defaultLocations defaultLocations { get; set; }
        public string companyLocation { get; set; }

    }
    public class lines
    {
        public string lineCode { get; set; }
        public string itemCode { get; set; }
        public int numberOfItems { get; set; }
        public double lineAmount { get; set; }
        public string itemDescription { get; set; }
        public string taxIncluded { get; set; }
        public int NumberOfNights { get; set; }
        public string avalaraGoodsAndServicesType { get; set; }
    }
    public class TaxRequest
    {
        public header header { get; set; }
        public lines[] lines { get; set; }
    }

}
