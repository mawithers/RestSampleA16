using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace RestSampleA16
{
    public static class GeneralTests
    {
        public static void RunGeneralTests()
        {

            string myRequest = File.ReadAllText(ConfigurationManager.AppSettings["InputFile"]);
            // Start stringing General Tests along by taking your Document for a walk

            // Calculate Tax on Document
            //myRequest = CalculateTest(, "Calculate");

            // Create Recorded Document from Calculated - TBD Next


            // Record Document
            myRequest = RecordTest(myRequest, "General");

            // Void Document 
            //GeneralTests.VoidTest(myRequest, "Voids");
            //GeneralTests.VoidTest(myRequest, "Voids");

            // UnVoid Document
           //GeneralTests.UnVoidTest(myRequest, "UnVoids");

            // Delete - Ask about Deletes

            
            // Keep walking the Document, Get the Document...


            //Utility.GetCompanies();

        }

        public static string CalculateTest(string request, string ravenDocument)
        {
            string response = Utility.GetTax(ConfigurationManager.AppSettings["CalculateUrl"], request);

            string myDocumentCode = Utility.GetDocumentCode(response.ToString());

            Utility.PutJsonInRavenDB("In-" + myDocumentCode, request, "myRequests" + ravenDocument);
            Utility.PutJsonInRavenDB("Out-" + myDocumentCode, response.ToString(), "myResponses" + ravenDocument);

            Console.WriteLine(response);

            return response;
        }

        public static string RecordTest(string request, string ravenDocument)
        {
            string myDocumentCode = Utility.GetDocumentCode(request.ToString());

            Utility.PutJsonInRavenDB("In-" + myDocumentCode, request, "myRequests" + ravenDocument);

            string response = Utility.GetTax(ConfigurationManager.AppSettings["Url"], request);
            
            Utility.PutJsonInRavenDB("Out-" + myDocumentCode, response.ToString(), "myResponses" + ravenDocument);

            Console.WriteLine(response);

            return response;
        }

        public static string RecordFromCalculatedTest(string request, string ravenDocument)
        {
            string response = Utility.GetTax(ConfigurationManager.AppSettings["Url"], request);

            string myDocumentCode = Utility.GetDocumentCode(response.ToString());

            Utility.PutJsonInRavenDB("In-" + myDocumentCode, request, "myRequests" + ravenDocument);
            Utility.PutJsonInRavenDB("Out-" + myDocumentCode, response.ToString(), "myResponses" + ravenDocument);

            Console.WriteLine(response);

            return response;
        }

        public static string VoidTest(string request, string ravenDocument)
        {
            string voidRequestBody = File.ReadAllText(ConfigurationManager.AppSettings["VoidFile"]);
            string response = "";

            // Pick the parts needed to construct the Url
            string myDocumentCode = Utility.GetDocumentCode(request.ToString());
            string myCompanyCode = Utility.GetCompanyCode(request.ToString());
            string myAccountId = Utility.GetAccountId(request.ToString());
            string myTransactionType = Utility.GetTransactionType(request.ToString());

            // https://tax.api.avalara.com/v2/transactions/account/{accountId}/company/{companyCode}/{transactionType}/{documentCode}/stateTransitions
            StringBuilder voidURl = new StringBuilder();
            voidURl.Append("https://tax.api.avalara.com/v2/transactions/account/");
            voidURl.Append(myAccountId);
            voidURl.Append("/company/");
            voidURl.Append(myCompanyCode + "/" + myTransactionType + "/" + myDocumentCode + "/stateTransitions");

            response = Utility.Void(voidURl.ToString(), voidRequestBody);

            Utility.PutJsonInRavenDB("In-" + myDocumentCode, request, "myRequests" + ravenDocument);
            Utility.PutJsonInRavenDB("Out-" + myDocumentCode, response.ToString(), "myResponses" + ravenDocument);

            Console.WriteLine(response);

            return response;
        }


        public static string UnVoidTest(string request, string ravenDocument)
        {
            string voidRequestBody = File.ReadAllText(ConfigurationManager.AppSettings["UnVoidFile"]);
            string response = "";

            // Pick the parts needed to construct the Url
            string myDocumentCode = Utility.GetDocumentCode(request.ToString());
            string myCompanyCode = Utility.GetCompanyCode(request.ToString());
            string myAccountId = Utility.GetAccountId(request.ToString());
            string myTransactionType = Utility.GetTransactionType(request.ToString());

            // https://tax.api.avalara.com/v2/transactions/account/{accountId}/company/{companyCode}/{transactionType}/{documentCode}/stateTransitions
            StringBuilder voidURl = new StringBuilder();
            voidURl.Append("https://tax.api.avalara.com/v2/transactions/account/");
            voidURl.Append(myAccountId);
            voidURl.Append("/company/");
            voidURl.Append(myCompanyCode + "/" + myTransactionType + "/" + myDocumentCode + "/stateTransitions");

            response = Utility.UnVoid(voidURl.ToString(), voidRequestBody);

            Utility.PutJsonInRavenDB("In-" + myDocumentCode, request, "myRequests" + ravenDocument);
            Utility.PutJsonInRavenDB("Out-" + myDocumentCode, response.ToString(), "myResponses" + ravenDocument);

            Console.WriteLine(response);

            return response;
        }

    }
}
