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
                Console.WriteLine("...Processing Started...");

                GeneralTests.RunGeneralTests();

                // Console.WriteLine(GrouponTests.FloridaTests(File.ReadAllText(ConfigurationManager.AppSettings["InputFile"]), "Florida")); 

                Console.WriteLine("...Processing Complete...");
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
