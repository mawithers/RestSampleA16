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
            // Start stringing General Tests along...Take your Document for a walk

            // Calculate

            // Transfer

            // Record
            string myRequest = GeneralTests.GetFromInputFile(File.ReadAllText(ConfigurationManager.AppSettings["InputFile"]), "General");

            // Void
            GeneralTests.VoidTest(myRequest, "Voids");

            // Delete

            // Keep walking the Document

            // Utility.GetCompanies();

        }

        public static string GetFromInputFile(string request, string ravenDocument)
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
    }
}
