using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TECAIS.IOT.ElectricMeteringUnit.Models;

namespace TECAIS.IOT.ElectricMeteringUnit.Services
{
    public class ElectricSubmissionService : IElectricSubmissionService
    {
        public readonly HttpClient _httpClient;

        public ElectricSubmissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PostHeatSubmission(ElectricSubmission submission)
        {

            var json = JsonSerializer.Serialize(submission);
            var content = new StringContent(json);

            HttpResponseMessage response = await _httpClient.PostAsync("url", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
