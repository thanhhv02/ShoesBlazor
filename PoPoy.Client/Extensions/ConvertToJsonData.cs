using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace PoPoy.Client.Extensions
{
    public static class FormatData
    {
        public static StringContent ToJsonBody(this object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            return stringContent;
        }

        public static string FormatAsPrice(object value)
        {

            return String.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:C}", decimal.Parse(value.ToString())).Replace("₫", "VNĐ");
        }
    }
}
