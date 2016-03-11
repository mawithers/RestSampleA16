using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json.Serialization;



namespace RestSampleA16
{
    class Program


    {
        static void Main(string[] args)
        {


            try
            {
                string requestDetail = File.ReadAllText(ConfigurationManager.AppSettings["InputFile"]);

                string details = Utility.PostJson(ConfigurationManager.AppSettings["Url"], requestDetail);
 
                string myDocumentCode = Utility.GetDocumentCode(details.ToString()); 

                //File.WriteAllText(myDocumentCode + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt", details.ToString());

                Utility.PutJsonInRavenDB("In-" + myDocumentCode, requestDetail , "myRequests");

                Utility.PutJsonInRavenDB("Out-" + myDocumentCode, details.ToString(), "myResponses");

                Console.Write(details.ToString());

            }

            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                 
            }

            finally
            {
                Console.ReadKey();
            }



        }


    }

}
