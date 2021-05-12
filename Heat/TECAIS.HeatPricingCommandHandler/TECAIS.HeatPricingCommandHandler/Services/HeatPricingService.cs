using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.HeatPricingCommandHandler.Services
{
    public class HeatPricingService : IHeatPricingService
    {
        private readonly HttpClient _htttpClient;
        private readonly ILogger _logger;
        public HeatPricingService(HttpClient client, ILogger logger)
        {
            _htttpClient = client;
            _logger = logger;
        }
        public async Task<double> GetPrice()
        {
            try
            {
                _logger.Information("Getting price from HeatPricingService");
                HttpResponseMessage response = await _htttpClient.GetAsync("url");

                response.EnsureSuccessStatusCode();

                string reponseContent = await response.Content.ReadAsStringAsync();

                return double.Parse(reponseContent);
            }
            catch (Exception ex)
            {
                throw new Exception("GetPrice failed with:", ex);
            }
        }

        
    }
}
