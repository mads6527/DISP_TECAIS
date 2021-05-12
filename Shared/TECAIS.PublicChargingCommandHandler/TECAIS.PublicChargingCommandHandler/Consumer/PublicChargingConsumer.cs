using MassTransit;
using ModelContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECAIS.PublicChargingCommandHandler.Services;

namespace TECAIS.PublicChargingCommandHandler.Consumer
{
    class PublicChargingConsumer : IConsumer<PublicChargingCommand>
    {
        private readonly IPublicChargingService _publicChargingService;
        private readonly ILogger _logger;
        public PublicChargingConsumer(IPublicChargingService service, ILogger logger)
        {
            _logger = logger;
            _publicChargingService = service;

        }
        public async Task Consume(ConsumeContext<PublicChargingCommand> context)
        {
            try
            {
                _logger.Information("Consuming message: {@message}", context.Message);

                var repsonse = await _publicChargingService.GetPrice();

            }
            catch (Exception e)
            {
                _logger.Error("Consumer error: ", e);
            }
        }
    }
}
