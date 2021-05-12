using MassTransit;
using ModelContracts;
using Serilog;
using System;
using System.Threading.Tasks;
using TECIAS.WaterPricingCommandHandler.Services;

namespace TECIAS.WaterPricingCommandHandler.Consumer
{
    public class WaterPriceConsumer : IConsumer<WaterPriceCommand>
    {
        private readonly IWaterPricingService _waterPricingService;
        private readonly ILogger _logger;

        public WaterPriceConsumer(IWaterPricingService waterPricingService, ILogger logger)
        {
            _waterPricingService = waterPricingService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WaterPriceCommand> context)
        {
            try
            {
                _logger.Information("Consuming Message {@Message}", context.Message);

                var reponse = await _waterPricingService.GetPrice();
            }
            catch (Exception e)
            {
                _logger.Error("Consumer Error: ", e);
            }
        }
    }
}
