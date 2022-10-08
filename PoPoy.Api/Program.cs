using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PoPoy.Api.Data;
using PoPoy.Api.Extensions;
using PoPoy.Shared.Dto;

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
                var UserManager = services.GetService<UserManager<User>>();
                var RoleManager = services.GetService<RoleManager<Role>>();
                new DbContextSeed(UserManager, RoleManager).SeedAsync(logger).Wait();

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
