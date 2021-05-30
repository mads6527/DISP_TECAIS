using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Models;

namespace TECAIS.IOT.HeatMeteringUnit.Services
{
    public interface IStatusSubmissionService
    {
        Task PostStatusSubmission(StatusSubmission submission);
    }
}
