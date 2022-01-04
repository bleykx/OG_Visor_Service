using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OG_Visor_Service.Classes;
using OG_Visor_Service;
using OG_Visor_Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using OG_Visor_Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Filters;
using Serilog.Events;

namespace OG_Visor_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<OGVisorService>();
                    services.AddHttpClient<HiveManager>();
                    services.AddHttpClient<Accountant>();
                    services.AddSingleton<Loggerizer>();
                });
    }
}
