using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RestSampleA16
{
    public static class GrouponTests
    {
        public static string FloridaTests(string request, string ravenDocument)
        {
            
            int rCnt = 0;
            int rCntStart = 1000;  //Drive from Config
            int rCntEnd = 1002;    //Drive from Config
            shipto myShipTo = new shipto();
            string response = "";

            for (rCnt = rCntStart; rCnt <= rCntEnd; rCnt++)
            {
                request = Utility.GrouponTaxRequest(myShipTo, request, rCnt);
                response = Utility.GetTax(ConfigurationManager.AppSettings["Url"], request);
                Utility.PutJsonInRavenDB("In-" + Utility.GetDocumentCode(request), request, "myRequest" + ravenDocument);
                Utility.PutJsonInRavenDB("Out-" + Utility.GetDocumentCode(request), response, "myResponse" + ravenDocument);
                Console.WriteLine(response);
            }
            return response;
        }
            
}
}
