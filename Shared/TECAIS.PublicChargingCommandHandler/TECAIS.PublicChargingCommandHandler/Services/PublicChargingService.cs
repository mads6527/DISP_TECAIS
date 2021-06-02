using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.PublicChargingCommandHandler.Services
{
    class PublicChargingService : IPublicChargingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public PublicChargingService(HttpClient client, ILogger logger)
        {
            _httpClient = client;
            _logger = logger;
        }

        public async Task<double> GetPrice()
        {
            try
            {
                _logger.Information("Getting price from PublicChargingService");
                HttpResponseMessage response = await _httpClient.GetAsync("http://swtdisp-grp10-public-charging-service/PublicCharging");

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
