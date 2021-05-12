using Automatonymous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.HeatSaga.StateMachines.TransactionSaga
{
    public class HeatSubmissionTransactionState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public State CurrentState { get; set; }
        public string CustomerAddress { get; set; }
    }
}
