
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Model;
using RestSharp;
using RestSharpClient;

namespace CoupangApi
{
    public abstract class CoupangApiBase
    {
        public static RestClient RestClient { get; } = RestSharpClientFactory.CreateClient(configureAction: option =>
        {
            option.BaseUrl = "https://api-gateway.coupang.com";
        });
    }

    public class ApiCollection : CoupangApiBase
    {
        public static async Task<BaseResponse<List<OrderSheets>>> GetOrdersheets(string vendorId, OrderSheetsRequest orderSheetsRequest)
        {
            vendorId = "C00456212";

            var resource = $"/v2/providers/openapi/apis/api/v4/vendors/{vendorId}/ordersheets";

            orderSheetsRequest = new OrderSheetsRequest
            {
                Status = "INSTRUCT",
                CreatedAtFrom = new DateTime(2021, 7, 1),
                CreatedAtTo = new DateTime(2021, 7, 31),
                MaxPerPage = 2
            };

            return await RestClient.PostAsync<List<OrderSheets>>(resource, orderSheetsRequest);
        }
    }
}
