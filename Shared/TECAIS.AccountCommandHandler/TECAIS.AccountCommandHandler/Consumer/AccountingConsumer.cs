using MassTransit;
using ModelContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECAIS.AccountCommandHandler.Services;

namespace TECAIS.AccountCommandHandler.Consumer
{
    class AccountingConsumer : IConsumer<AccountingCommand>
    {
        private readonly IAccountingService _accountingService;
        private readonly ILogger _logger;
        AccountingConsumer(IAccountingService service, ILogger logger)
        {
            _logger = logger;
            _accountingService = service;

        }
        public async Task Consume(ConsumeContext<AccountingCommand> context)
        {
            try
            {
                _logger.Information("Consuming message: {@message}", context.Message);

                var repsonse = await _accountingService.UpdateAccount(context.Message);

            }
            catch (Exception e)
            {
                _logger.Error("Consumer error: ", e);
            }
        }
    }
}
