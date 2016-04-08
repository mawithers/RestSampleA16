using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSampleA16
{
    
public class Address
    {
        public string line1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string zipcode { get; set; }
        public string municipality { get; set; }
        public string town { get; set; }
        public string province { get; set; }
        public string postcode { get; set; }
    }

    public class Latlong
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class ShipTo
    {
        public Address address { get; set; }
        public Latlong latlong { get; set; }
        public string resolutionQuality { get; set; }
    }

    
    
    public class ShipFrom
    {
        public Address address { get; set; }
        public Latlong latlong { get; set; }
        public string resolutionQuality { get; set; }
    }

    public class DefaultLocations
    {
        public ShipTo shipTo { get; set; }
        public ShipFrom shipFrom { get; set; }
    }

    public class Metadata
    {
    }

    public class Header
    {
        public string documentCode { get; set; }
        public DefaultLocations defaultLocations { get; set; }
        public string defaultTaxPayerCode { get; set; }
        public double totalTaxOverrideAmount { get; set; }
        public string transactionType { get; set; }
        public string accountingEntityCode { get; set; }
        public string companyCode { get; set; }
        public string customerCode { get; set; }
        public string accountId { get; set; }
        public string transactionDate { get; set; }
        public string currency { get; set; }
        public string taxCalculationDate { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Sales
    {
        public double tax { get; set; }
    }

    public class TaxByType
    {
        public Sales sales { get; set; }
    }

    public class Detail
    {
        public string taxType { get; set; }
        public string rateType { get; set; }
        public double subtotalTaxable { get; set; }
        public string scenario { get; set; }
        public double subtotalExempt { get; set; }
        public double rate { get; set; }
        public double tax { get; set; }
        public string locationType { get; set; }
        public List<object> significantLocations { get; set; }
        public string jurisdictionName { get; set; }
        public string jurisdictionType { get; set; }
        public string taxRuleType { get; set; }
    }

    public class CalculatedTax
    {
        public TaxByType taxByType { get; set; }
        public double tax { get; set; }
        public List<Detail> details { get; set; }
        public List<object> calculatedDetails { get; set; }
    }

    
    
    public class Locations
    {
        public ShipTo shipTo { get; set; }
        public ShipFrom shipFrom { get; set; }
    }

    
    public class Line
    {
        public CalculatedTax calculatedTax { get; set; }
        public string taxPayerCode { get; set; }
        public string lineCode { get; set; }
        public string itemCode { get; set; }
        public string avalaraGoodsAndServicesType { get; set; }
        public double numberOfItems { get; set; }
        public double lineAmount { get; set; }
        public bool taxIncluded { get; set; }
        public string itemDescription { get; set; }
        public Locations locations { get; set; }
        public Metadata metadata { get; set; }
        public string avalaraGoodsAndServicesModifierType { get; set; }
        public string form { get; set; }
        public string domain { get; set; }
        public string IsSSTCertified { get; set; }
        public string state { get; set; }
        public string Nature { get; set; }
        public string transactionType { get; set; }
    }

    public class Jurisdiction
    {
        public string jurisdictionName { get; set; }
        public string jurisdictionType { get; set; }
        public double tax { get; set; }
    }
 
    public class CalculatedTaxSummary
    {
        public int numberOfLines { get; set; }
        public TaxByType taxByType { get; set; }
        public double subtotal { get; set; }
        public double totalTax { get; set; }
        public double grandTotal { get; set; }
    }

    public class ProcessingInfo
    {
        public string transactionState { get; set; }
        public string versionId { get; set; }
        public int formatId { get; set; }
        public double duration { get; set; }
        public string modifiedDate { get; set; }
        public string documentId { get; set; }
    }

    public class TaxResponse
    {
        public Header header { get; set; }
        public List<Line> lines { get; set; }
        public CalculatedTaxSummary calculatedTaxSummary { get; set; }
        public ProcessingInfo processingInfo { get; set; }
    }

}
