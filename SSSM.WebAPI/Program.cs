using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SSSM.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
             .ConfigureLogging((hostingContext, logging) =>
             {                
                 var log4netConfigFileName = hostingContext.HostingEnvironment.IsDevelopment() ? "log4net.development.config" : "log4net.config";
                 logging.AddLog4Net(log4netConfigFileName);
                 //logging.SetMinimumLevel(LogLevel.Debug);
             });
    }
}