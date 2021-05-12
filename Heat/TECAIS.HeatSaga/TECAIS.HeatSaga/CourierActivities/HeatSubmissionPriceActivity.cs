using MassTransit;
using MassTransit.Courier;
using ModelContracts;
using Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.HeatSaga.CourierActivities
{
    public class HeatSubmissionPriceActivity : IActivity<HeatSubmissionPriceArguments, HeatSubmissionLog>
    {
        //from request get response
        readonly IRequestClient<HeatSubmission> _client;

        public HeatSubmissionPriceActivity(IRequestClient<HeatSubmission> client)
        {
            _client = client;
        }

        //
        public Task<CompensationResult> Compensate(CompensateContext<HeatSubmissionLog> context)
        {
            throw new NotImplementedException();
        }

        //What goes out and calls the service in the other component.
        //Do argument
        public async Task<ExecutionResult> Execute(ExecuteContext<HeatSubmissionPriceArguments> context)
        {
            var response = await _client.GetResponse<WaterPriceResult>()
        }
    }

    //input for the activity
    public interface HeatSubmissionPriceArguments 
    {
        double HeatConsumption { get; }
    }
    //want to keep trackof
    public interface HeatSubmissionLog 
    {
        Guid HeatConsumptionId { get; }
    }
}
