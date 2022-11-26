using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PoPoy.Client.Extensions;
using PoPoy.Shared.Common;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;
namespace PoPoy.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjA2MDc1QDMyMzAyZTMxMmUzME90Vy9QRVJFcG1mMmRZUEtqOWtZeEpvRmxPM29RdlJaNHkyN0xieXpVdlU9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddServices();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddNugetPackageServices();
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["BackendApiUrl"])
            }.EnableIntercept(sp));
            builder.Services.AddHttpClientInterceptor();
            builder.Services.PostConfigure<LoggerFilterOptions>(opt =>
    opt.Rules.Add(
        new LoggerFilterRule(null, "Microsoft.AspNetCore.Authorization.*", LogLevel.None, null)
        ));
            builder.Services.AddSyncfusionBlazor();
            await builder.Build().RunAsync();
        }
    }
}
