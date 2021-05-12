using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECIAS.ElectricityPricingCommandHandler.Model
{
    public interface ElectricityPriceCommand
    {
        public double Price { get; set; }
    }
}
