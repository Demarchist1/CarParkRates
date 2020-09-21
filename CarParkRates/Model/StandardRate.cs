using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkRates.Model
{
    public class StandardRate : BaseRate
    {
        public StandardRate(DateTime entry, DateTime exit)
            : base("Standard", entry, exit) { }

        public override decimal Rate
        {
            get
            {
                TimeSpan t = Exit - Entry;
                if (t.Days == 0 && t.Hours < 3)
                    return (t.Hours + 1) * 5;
                else
                    return (t.Days + 1) * 20;
            }
        }

        public override bool IsValid
        {
            get
            {
                return true;
            }
        }
    }
}
