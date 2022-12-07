using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace PoPoy.Client.Helper
{
    public static class ConvertToJsonData
    {
        public static StringContent ToJsonBody(this object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            return stringContent;
        }

        public static string ConvertToUnSign(this string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2.ToLower();
        }

     
            public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
            {
                return listToClone.Select(item => (T)item.Clone()).ToList();
            }
  
    }

    public static class AppExtensions
    {
        public static string FormatAsPrice(object value)
        {

            return String.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:C}", decimal.Parse(value.ToString()));
        }
    }
}
