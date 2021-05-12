using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.HeatPricingCommandHandler.Model
{
    public interface HeatPriceCommand
    {
        public double Price { get; set; }

    }
}
