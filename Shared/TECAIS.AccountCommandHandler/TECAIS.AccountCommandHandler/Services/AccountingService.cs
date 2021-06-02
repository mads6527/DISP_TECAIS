using ModelContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TECAIS.AccountCommandHandler.Services
{
    class AccountingService : IAccountingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public AccountingService(HttpClient client, ILogger logger)
        {
            _httpClient = client;
            _logger = logger;
        }
        public async Task<HttpStatusCode> UpdateAccount(AccountingCommand command)
        {
            try
            {
                _logger.Information("Updating account with new information");

                string jsonString = JsonSerializer.Serialize(command);

                HttpRequestMessage message = new HttpRequestMessage();
                message.RequestUri = new Uri("http://swtdisp-grp10-accounting-service/Accounting");
                message.Content = new StringContent(jsonString);
                message.Method = new HttpMethod("POST");

                HttpResponseMessage response = await _httpClient.SendAsync(message);

                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
            catch (Exception e)
            {
                throw new Exception("UpdateAccount failed with: ",e);
            }
        }
    }
}
