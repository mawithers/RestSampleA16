using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;


namespace RestSampleA16  
{
    public static class Utility
    {
        public static string PostJson(string url, string body)
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

            return result;
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
        public static void PutJsonInRavenDB(string json)
        {
            string databaseName = "A16Documents";
            int docId = 1;
            string entityName = "Documents";
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
    }
}
