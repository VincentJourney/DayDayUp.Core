using System;
using System.IO;
using System.Security.Cryptography;

namespace CoupangApi
{
    public class Hmac
    {
        private const string Algorithm = "HmacSHA256";

        public static string Get(string path, string method, string query)
        {
            string datetime = DateTime.Now.ToUniversalTime().ToString("yyMMddTHHmmssZ");
            string message = GenerateFormattedMessage(path, datetime, method, query);
            string signature = CreateTokenByHMACSHA256(message);
            return GenerateFormattedHeader(datetime, signature);
        }

        private static string GenerateFormattedMessage(string path, string datetime, string method, string query)
        {
            return String.Format("{0}{1}{2}{3}", datetime, method, path, query);
        }

        private static String GenerateFormattedHeader(string datetime, string signature)
        {
            return String.Format("CEA algorithm={0}, access-key={1}, signed-date={2}, signature={3}", Algorithm, GlobalConfig.AccessKey, datetime, signature);
        }

        private static String CreateTokenByHMACSHA256(string message)
        {
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(GlobalConfig.SecretKey);
            var hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            return ByteToString(hmacsha256.ComputeHash(messageBytes));
        }

        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("x2"); // hex format
            }

            return sbinary;
        }
    }
}
