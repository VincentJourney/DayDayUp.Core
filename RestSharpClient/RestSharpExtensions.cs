using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Model;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharpClient
{
    public static class RestSharpExtensions
    {
        public static async Task<BaseResponse<T>> GetAsync<T>(this RestClient client, string resource, object requestModel) where T : class
        {
            var response = new BaseResponse<T>();
            try
            {
                var method = Method.GET;
                IRestRequest req = new RestRequest(resource, method)
                   .AddQueryParameter(requestModel)
                   .AddAuthorization(method, resource);

                var res = await client.ExecuteAsync<BaseResponse<T>>(req);

                if (res.StatusCode != HttpStatusCode.OK || !res.IsSuccessful)
                {
                    response.Code = (int)res.StatusCode;
                    response.Message = res.GetResponseErrorMessage();
                    return response;
                }

                return res.Data;
            }
            catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }

            return response;
        }

        public static async Task<BaseResponse<T>> PostAsync<T>(this RestClient client, string resource, object requestModel) where T : class
        {
            var response = new BaseResponse<T>();
            try
            {
                var method = Method.POST;
                IRestRequest req = new RestRequest(resource, method)
                   .AddJsonBody(requestModel)
                   .AddAuthorization(method, resource);

                var res = await client.ExecuteAsync<BaseResponse<T>>(req);

                if (res.StatusCode != HttpStatusCode.OK || !res.IsSuccessful)
                {
                    response.Code = (int)res.StatusCode;
                    response.Message = res.GetResponseErrorMessage();
                    return response;
                }

                return res.Data;
            }
            catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }

            return response;
        }

        private static IRestRequest AddQueryParameter(this IRestRequest restRequest, object requestModel)
        {
            if (requestModel == null)
            {
                return restRequest;
            }

            var properties = requestModel.GetType().GetProperties();
            foreach (var property in properties)
            {
                object value = property.GetValue(requestModel);
                if (value == null) continue;

                string valueAsString;
                if (value is DateTime dtValue)
                {
                    valueAsString = dtValue.ToString("yyyy-MM-dd");
                }
                else
                {
                    valueAsString = value.ToString();
                }

                restRequest.AddQueryParameter(property.Name, valueAsString);
            }

            return restRequest;
        }

        private static IRestRequest AddAuthorization(this IRestRequest req, Method method, string resource)
        {
            var query = string.Empty;
            if (method == Method.GET)
            {
                query = $"{string.Join("&", req.Parameters.Where(s => s.Type == ParameterType.QueryString))}";
            }

            var hmac = Hmac.Get(resource, method.ToString(), query);
            req.AddHeader("Authorization", hmac);

            return req;
        }

        private static string GetResponseErrorMessage(this IRestResponse res)
        {
            return $@"
[Url] : {res.ResponseUri.ToString()} 
[HttpStatusCode] ：{res.StatusCode }
[ErrorMessage] : {res.ErrorMessage} 
[Content]：{res.Content}
";
        }
    }
}
