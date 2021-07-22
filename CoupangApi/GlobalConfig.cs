using System;
using System.Collections.Generic;
using System.Text;

namespace CoupangApi
{
    public class GlobalConfig
    {
        public static string Url { get; set; } = "https://api-gateway.coupang.com";
        public static string Schema { get; set; } = "https";
        public static int Port { get; set; } = 443;
        //public static string Path { get; set; } = "/v2/providers/seller_api/apis/api/v1/marketplace/seller-products/48773047";

        //replace with your own accessKey
        public static string AccessKey { get; set; } = "b6dac2c8-1ed9-4574-bb26-4e53f6da0df1";
        //replace with your own secretKey
        public static string SecretKey { get; set; } = "d0ed27c4cbb746167251b440c3bd726f2e037c14";


    }
}
