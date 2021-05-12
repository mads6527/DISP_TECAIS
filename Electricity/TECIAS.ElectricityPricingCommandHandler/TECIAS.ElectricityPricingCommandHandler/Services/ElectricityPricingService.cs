using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TECIAS.ElectricityPricingCommandHandler.Services
{
    class ElectricityPricingService : IElectricityPricingService
    {
        private readonly HttpClient _htttpClient;
        private readonly ILogger _logger;

        public ElectricityPricingService(HttpClient client, ILogger logger)
        {
            _htttpClient = client;
            _logger = logger;
        }

        public Task<double> GetPrice()
        {
            throw new NotImplementedException();
        }
    }
}
