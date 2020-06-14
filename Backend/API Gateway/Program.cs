using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace API_Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, config) =>
                    {
                        config.AddJsonFile("configuration.json");
                    }).ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                    logging.AddConsole();
                }).UseSerilog((_, config) =>
                {
                    config
                        .MinimumLevel.Verbose()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day);
                }).UseStartup<Startup>();
    }
}
