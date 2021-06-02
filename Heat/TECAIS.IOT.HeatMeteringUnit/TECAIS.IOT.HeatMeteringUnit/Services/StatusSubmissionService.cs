using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Models;

namespace TECAIS.IOT.HeatMeteringUnit.Services
{
    public class StatusSubmissionService : IStatusSubmissionService
    {
        public readonly HttpClient _httpClient;

        public StatusSubmissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PostStatusSubmission(StatusSubmission submission)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            var content = new StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await _httpClient.PostAsync("https://heat_status_service:443/StatusSubmission", content);
            HttpResponseMessage response = await _httpClient.PostAsync("http://swtdisp-grp10-heat-status-submission:80/StatusSubmission", content); 
            response.EnsureSuccessStatusCode();
        }
    }
}
