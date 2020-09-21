using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParkRates.Model
{
    public class RateResult
    {
        public RateResult(BaseRate calculated)
            :this(calculated.Name, calculated.Rate) { }

        public RateResult(string name, decimal rate)
        {
            Name = name;
            Rate = rate;
        }

        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
