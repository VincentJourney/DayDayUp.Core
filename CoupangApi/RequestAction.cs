
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace CoupangApi
{
    public class RequestAction
    {
        public static void Test()
        {
            var vendorId = "C00456212";
            var path = $"/v2/providers/openapi/apis/api/v4/vendors/{vendorId}/ordersheets";

            Get(path);
        }

        public static void Get()
        {
            //restsha
        }


        public static void Get(string path)
        {
            string method = "GET";

            var uriBuilder = new UriBuilder(GlobalConfig.Url + path);
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["createdAtFrom"] = "2021-07-01";
            parameters["createdAtTo"] = "2021-07-31";
            parameters["status"] = "INSTRUCT";
            parameters["maxPerPage"] = "2";
            uriBuilder.Query = parameters.ToString();

            string query = uriBuilder.Query.ToString().Remove(0, 1);

            uriBuilder.Scheme = GlobalConfig.Schema;
            uriBuilder.Port = GlobalConfig.Port;
            Uri finalUrl = uriBuilder.Uri;

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(finalUrl.ToString());

                request.Timeout = 10000;
                request.Method = method;

                request.ContentType = "application/json;charset=UTF-8";
                var hmac = Hmac.Get(path, method, query);
                request.Headers["Authorization"] = hmac;

                var response = (HttpWebResponse)request.GetResponse();
                // Display the status ...
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Response Status Code is OK and StatusDescription is: {0}", response.StatusDescription);

                    var responseStream = response.GetResponseStream();

                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    StreamReader reader = new StreamReader(responseStream, encode);

                    string responseString = reader.ReadToEnd();

                    reader.Close();
                    responseStream.Close();
                    response.Close();

                    Console.WriteLine(responseString);
                    //Console.WriteLine(String.Format("Response: {0}", responseString));
                }
                else
                {
                    Console.WriteLine("Response Status Code is Not OK and StatusDescription is: {0}", response.StatusDescription);
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("WebException Raised. The following error occured : {0}", e.Status);
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("The following Exception was raised : {0}", e.Message);
            }

        }
    }
}
