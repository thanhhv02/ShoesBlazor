using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace PoPoy.Client.Extensions
{
    public static class ConvertToJsonData
    {
        public static StringContent ToJsonBody(this object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            return stringContent;
        }
    }
}
