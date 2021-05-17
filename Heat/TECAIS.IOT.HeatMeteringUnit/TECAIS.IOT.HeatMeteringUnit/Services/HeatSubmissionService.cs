using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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
            var content = new StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://172.28.0.3:80/HeatSubmission", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
