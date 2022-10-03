using Microsoft.AspNetCore.Http;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PoPoy.Api.VNPay
{
    public class Util
    {
        private readonly IHttpContextAccessor contextAccessor;
        public Util(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        public string GetIpAddress()
        {
            string ipAddress;
            try
            {
                ipAddress = contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown"))
                    ipAddress = contextAccessor.HttpContext.Connection.LocalIpAddress.ToString();
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }

            return ipAddress;
        }
    }
}
