using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.Serialization;
using System.IO;



namespace RestSampleA16
{
    class Program


    {
        static void Main(string[] args)
        {


            try
            {
                string details = Utility.PostJson(ConfigurationManager.AppSettings["Url"], File.ReadAllText("GetTax.txt"));
                Console.Write(details.ToString());

                string documentId = Utility.GetValueBetween(details, "#", "@");
                File.WriteAllText(documentId + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt", details.ToString());

                Utility.PutJsonInRavenDB(details.ToString());
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
