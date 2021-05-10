using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.HeatSubmissionService.Models
{
    public class HeatSubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float HeatConsumption { get; set; }
    }
}
