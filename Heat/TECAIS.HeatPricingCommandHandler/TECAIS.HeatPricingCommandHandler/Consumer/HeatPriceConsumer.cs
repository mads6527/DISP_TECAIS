using MassTransit;
using Serilog;
using System;
using System.Threading.Tasks;
using TECAIS.HeatPricingCommandHandler.Model;
using TECAIS.HeatPricingCommandHandler.Services;

namespace TECAIS.HeatPricingCommandHandler.Consumer
{
    class HeatPriceConsumer : IConsumer<HeatPriceCommand>
    {
        private readonly IHeatPricingService _heatPricingService;
        private readonly ILogger _logger;
        public HeatPriceConsumer(IHeatPricingService service, ILogger logger)
        {
            _logger = logger;
            _heatPricingService = service;
            
        }
        public async Task Consume(ConsumeContext<HeatPriceCommand> context)
        {
            try
            {
                _logger.Information("Consuming message: {@message}", context.Message);

                var repsonse = await _heatPricingService.GetPrice();
                
            }
            catch (Exception e)
            {
                _logger.Error("Consumer error: ", e);
            }
        }
    }
}
