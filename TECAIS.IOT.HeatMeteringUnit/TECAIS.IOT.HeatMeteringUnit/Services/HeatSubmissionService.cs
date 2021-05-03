using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Models;

namespace TECAIS.IOT.HeatMeteringUnit.Services
{
    public class HeatSubmissionService : IHeatSubmissionService
    {
        public readonly HttpClient _httpClient;

        public HeatSubmissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PostHeatSubmission(HeatSubmission submission)
        {

            var json = JsonSerializer.Serialize(submission);
            var content = new StringContent(json);

            HttpResponseMessage response = await _httpClient.PostAsync("url", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
