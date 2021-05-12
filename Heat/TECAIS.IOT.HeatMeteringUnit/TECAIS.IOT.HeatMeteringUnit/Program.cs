using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Services;

namespace TECAIS.IOT.HeatMeteringUnit
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                        config.AddCommandLine(args);

                }).ConfigureServices((hostingContext, services) =>
                {
                    //Add dependencies to service collection
                    services.AddHostedService<UnitConsoleHostedService>();
                    services.AddHttpClient<IHeatSubmissionService, HeatSubmissionService>();

                }).ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddSerilog(dispose: true);
                });


            if (isService)
                await builder.UseWindowsService().Build().RunAsync();
            else
                await builder.RunConsoleAsync();

            Log.CloseAndFlush();
        }
    }
}
