using Automatonymous;
using Contracts;
using MassTransit.Saga;
using Serilog;
using System;

namespace TECAIS.HeatSaga.StateMachines
{
    public class HeatSubmissionStateMachine : MassTransitStateMachine<SubmissionState>
    {
        public HeatSubmissionStateMachine(ILogger logger)
        {
            //persisting the state -> create a new instance if it does not exsist
            Event(() => OnOrderSubmitted, x => x.CorrelateById(m => m.Message.Id));

            InstanceState(x => x.CurrentState);

            //first state - final state (last state) When onOrderSubmitted go to state Submitted
            Initially(When(OnOrderSubmitted)
                .Then(context => context.Instance.CustomerAddress = context.Data.CustomerAddress)
                .TransitionTo(Submitted));

            

            During(Submitted, Ignore(OnOrderSubmitted));
        }

        public State Submitted { get; private set; }

        //define that we can handle det different events for the message
        public Event<HeatSubmitted> OnOrderSubmitted { get; private set; }
        public Event<HeatPricingCommand> OnHeatPricingCommand { get; private set; }
    }

    public class SubmissionState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId  { get; set; }
        public string CurrentState { get; set; }
        //needed for redis
        public int Version { get; set; }
        public string CustomerAddress { get; set; }
    }

    //use redis to persist state
}
