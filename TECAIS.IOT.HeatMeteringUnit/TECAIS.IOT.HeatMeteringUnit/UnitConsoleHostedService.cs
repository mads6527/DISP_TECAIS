using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Services;

namespace TECAIS.IOT.HeatMeteringUnit
{
    public class UnitConsoleHostedService : IHostedService
    {
        private readonly IHeatSubmissionService _heatSubmissionService;
        private int? _exitCode;

        public UnitConsoleHostedService(IHeatSubmissionService heatSubmissionService)
        {
            _heatSubmissionService = heatSubmissionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting");
            var measurement = 10;

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Posting");
                measurement = measurement + 1;

                await _heatSubmissionService.PostHeatSubmission(new Models.HeatSubmission
                {
                    Address = "Krusaavej29",
                    TimeOfMeasurement = DateTime.Now,
                    HeatComsumtion = measurement
                });

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //implement stop function 
            Console.WriteLine("Stopping");

            // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}
