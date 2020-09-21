using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkRates.Model
{
    public class WeekendRate : BaseRate
    {
        public WeekendRate(DateTime entry, DateTime exit)
            : base("Weekend", entry, exit) { }

        public override decimal Rate
        {
            get { return 10m; }
        }

        public override bool IsValid
        {
            get
            {
                // after midnight friday = saturday
                switch (Entry.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        switch (Exit.DayOfWeek)
                        {
                            case DayOfWeek.Saturday:
                            case DayOfWeek.Sunday:
                                if ((Exit.Date - Entry.Date).Days < 3)
                                    return true; // same weekend, not the following week 
                                break;
                        }
                        break;
                }

                return false;
            }
        }
    }
}
