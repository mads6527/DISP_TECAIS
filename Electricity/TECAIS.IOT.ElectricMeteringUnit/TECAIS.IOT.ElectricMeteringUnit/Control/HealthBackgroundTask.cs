using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TECAIS.IOT.ElectricMeteringUnit.Control
{
    public class HealthBackgroundTask : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("test");

                //await run job

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("start");

            await ExecuteAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("stop");

            return Task.CompletedTask;
        }
    }
}
