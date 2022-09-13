using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PoPoy.Api.Data;
using PoPoy.Api.Extensions;

namespace PoPoy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.MigrateDbContext<DataContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<DbContextSeed>>();
                new DbContextSeed().SeedAsync(context, logger).Wait();
            });
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



    }
}
