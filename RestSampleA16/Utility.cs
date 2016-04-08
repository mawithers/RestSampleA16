using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;


namespace RestSampleA16  
{
    public static class Utility
    {
        private static string CreateWebRequest(string url, string body)
        {

            try
            {
                HttpWebRequest req = WebRequest.Create(new Uri(url))
                                      as HttpWebRequest;

                req.Method = "POST";

                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentType = "application/json";
                req.Headers.Add("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                req.Accept = "application/json; document-version=1";
                req.UserAgent = ConfigurationManager.AppSettings["UserAgent"];

                // Encode the parameters as form data:
                byte[] formData = UTF8Encoding.UTF8.GetBytes(body);
                req.ContentLength = formData.Length;

                // Send the request:  You can use the "using" function on classes implementing IDispose interface. 
                // Using will call Dispose method when that object goes out of scope and not wait on GC.
                using (Stream post = req.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }

                // Pick up the response:
                string result = null;
                using (HttpWebResponse resp = req.GetResponse()
                                              as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }

                // test for no content...If no content add brackets and see with the minimum is for Raven {}
                if (result.Length == 0) { result = "{}"; }


                return result;

                
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            return reader.ReadToEnd().ToString();
                        }
                    }
                }
                return wex.Message;
            }
        }

        public static string GetTax(string url, string body)
        {

              return CreateWebRequest(url, body);
            
        }

        public static string GetValueBetween(string strSource, string strStart, string strEnd)
        {

            var regex = new Regex(".* " + strStart + "(.*) " + strEnd + ".*");
            if (regex.IsMatch(strSource))
            {
                var myCapturedText = regex.Match(strSource).Groups[1].Value;
                Console.WriteLine("This is my captured text: {0}", myCapturedText);
            }

            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }


        public static void GetCompanies()
        {

            // Pick up the response:
            HttpWebRequest req = WebRequest.Create(new Uri("https://aic3.api.avalara.com/companies"))
                                  as HttpWebRequest;

            req.Method = "GET";
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            req.Accept = "application/json; document-version=1";
            req.UserAgent = ConfigurationManager.AppSettings["UserAgent"];

            
                // Pick up the response:
                string result = null;
                using (HttpWebResponse resp = req.GetResponse()
                                              as HttpWebResponse)
           
            Console.Write(result);
        }

        public static void PutJsonInRavenDB(string documentCode, string json, string entityName)
        {
            string databaseName = "A16Documents";
            string docId = documentCode;
            var url = string.Format(ConfigurationManager.AppSettings["RavenDBUrl"], databaseName, docId);
            var webRequest = System.Net.HttpWebRequest.CreateHttp(url);
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/json";
            webRequest.Headers["Raven-Entity-Name"] = entityName;
            var stream = webRequest.GetRequestStream();
            using (var writer = new System.IO.StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.Write(json);
                }
            var webResponse = webRequest.GetResponse();
            webResponse.Close();
        }

        public static string GrouponTaxRequest(shipto taxAddress, string taxText,int rowStart)
        {
            TaxRequest myTaxRequest = new TaxRequest();
            header myHeader = new header();
            address myAddress = new address();
            List<lines> myLines = new List<lines>();

            lines firstLine = new lines();
            firstLine.itemDescription = "VBRO";
            firstLine.lineCode = "1";
            firstLine.itemCode = ExcelUtil.ReadData(rowStart, 3);
            firstLine.lineAmount = 450.50;
            firstLine.taxIncluded = "false";
            firstLine.NumberOfNights = 7; 
            firstLine.numberOfItems = 2;
            firstLine.avalaraGoodsAndServicesType = "LDG000001";

            myLines.Add(firstLine);

            myHeader.accountId = GetAccountId(taxText); 
            myHeader.documentCode = GetDocumentCode();  //Set to a Guid
            myHeader.companyCode = GetCompanyCode(taxText);
            myHeader.companyLocation = "HQ";
            myHeader.customerCode = ExcelUtil.ReadData(rowStart, 1);
            myHeader.transactionType = "Sale"; // Get from Config
            DateTime dt = DateTime.Today;
            myHeader.transactionDate = String.Format("{0:yyyy-M-d}", dt);

            myAddress.line1 = ExcelUtil.ReadData(rowStart,2);
            myAddress.city = ExcelUtil.ReadData(rowStart,3);
            myAddress.country = "US"; 
            myAddress.zipcode = ExcelUtil.ReadData(rowStart,6);

            taxAddress.address = myAddress;         

            defaultLocations mydefaultLocations = new defaultLocations();
            mydefaultLocations.POS = taxAddress;
           // mydefaultLocations.ShipFrom = taxAddress;

            myHeader.defaultLocations = mydefaultLocations;

            myTaxRequest.header = myHeader;
            myTaxRequest.lines = myLines.ToArray();

            //Console.Write(Newtonsoft.Json.JsonConvert.SerializeObject(myTaxRequest));

            return Newtonsoft.Json.JsonConvert.SerializeObject(myTaxRequest);

        }

        public static string GetDocumentCode(string json)
        {
            JObject joResponse = JObject.Parse(json);
  
            JObject ojObject = (JObject)joResponse["header"];
            
            //JArray array = (JArray)ojObject["chats"];
            
            if (ojObject == null) 
                {
                    return "InvalidRequest";
                }
            else
                {
                    return ojObject.SelectToken("documentCode").ToString();
               
                }  
             
        }


        public static string GetAccountId(string json)
        {
            JObject joResponse = JObject.Parse(json);

            JObject ojObject = (JObject)joResponse["header"];

            if (ojObject == null)
            {
                return "InvalidRequest";
            }
            else
            {
                return ojObject.SelectToken("accountId").ToString();

            }

        }

        public static string GetTransactionType(string json)
        {
            JObject joResponse = JObject.Parse(json);

            JObject ojObject = (JObject)joResponse["header"];

            if (ojObject == null)
            {
                return "InvalidRequest";
            }
            else
            {
                return ojObject.SelectToken("transactionType").ToString();

            }

        }
        public static string GetCompanyCode(string json)
        {
            JObject joResponse = JObject.Parse(json);

            JObject ojObject = (JObject)joResponse["header"];

            if (ojObject == null)
            {
                return "InvalidRequest";
            }
            else
            {
                return ojObject.SelectToken("companyCode").ToString();

            }

        }
        public static string Void(string url, string body)
        {
                
                return CreateWebRequest(url, body);
           
        }
    public static string UnVoid(string documentCode)
        {

            return documentCode;

         }


public static string GetCustomerCode(string json)
        {
            JObject joResponse = JObject.Parse(json);

            JObject ojObject = (JObject)joResponse["header"];

            if (ojObject == null)
            {
                return "InvalidRequest";
            }
            else
            {
                return ojObject.SelectToken("customerCode").ToString();

            }

        }
        public static string GetDocumentCode()
        {
           
                return Guid.NewGuid().ToString(); ;

        }
    }
}
