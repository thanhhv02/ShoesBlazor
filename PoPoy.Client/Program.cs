using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PoPoy.Client.Extensions;
using PoPoy.Client.Services.AuthService;
using PoPoy.Client.Services.ProductService;
using PoPoy.Shared.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoPoy.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddServices();
            builder.Services.AddNugetPackageServices();
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["BackendApiUrl"])
            });
            await builder.Build().RunAsync();
        }
    }
}
