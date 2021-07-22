using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using RestSharp;

namespace RestSharpClient
{
    public static class RestSharpExtensions
    {
        public static async Task Get<T>(this RestClient client, T requestModel) where T : class
        {
            var req = new RestRequest();
            req = req.AddQueryParameter(requestModel);
            req.Method = Method.GET;
            var res = await client.ExecuteAsync(req);
            Console.WriteLine(res.Content);
        }

        private static RestRequest AddQueryParameter<T>(this RestRequest restRequest, T requestModel) where T : class
        {
            var properties = requestModel.GetType().GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name.ToCamel();
                object value = property.GetValue(requestModel);
                string valueAsString;
                if (value is DateTime dtValue)
                {
                    valueAsString = dtValue.ToString("yyyy-MM-dd");
                }
                else
                {
                    valueAsString = value.ToString();
                }

                restRequest.AddQueryParameter(name, valueAsString);
            }

            return restRequest;
        }

        private static string ToCamel(this string sentence)
        {
            return sentence.FirstOrDefault().ToString().ToLower() + sentence.Substring(1);
        }
    }
}
