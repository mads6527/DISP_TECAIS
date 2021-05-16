﻿using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.HeatSaga.StateMachines;

namespace TECAIS.HeatSaga
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
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddSagaStateMachine<HeatSubmissionStateMachine, SubmissionState>()
                            .InMemoryRepository();

                        cfg.UsingRabbitMq(ConfigureBus);
                    });

                    services.AddHostedService<HeatSageConsoleHostedService>();
                    
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddSerilog(dispose: true);
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                });

            if (isService)
                await builder.UseWindowsService().Build().RunAsync();
            else
                await builder.RunConsoleAsync();

            Log.CloseAndFlush();
        }

        static void ConfigureBus(IBusRegistrationContext busRegistrationContext, IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            configurator.ReceiveEndpoint("HeatSaga", e =>
            {
                e.ConfigureSaga<SubmissionState>(busRegistrationContext);
            });

            configurator.ConfigureEndpoints(busRegistrationContext);
        }
    }
}
