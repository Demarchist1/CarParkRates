using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkRates.Model
{
    public class NightRate : BaseRate
    {
        public NightRate(DateTime entry, DateTime exit)
            : base("Night", entry, exit) { }

        public override decimal Rate
        {
            get { return 6.5m; }
        }

        public override bool IsValid
        {
            get
            {
                switch (Entry.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        if (Entry.Hour >= 18 && Entry.Hour <= 23)
                        {
                            if (Exit.Date == Entry.Date) // leave the same day before midnight
                                return true;
                            else if ((Exit.Date - Entry.Date).Days == 1 //leave the next day
                                && Exit.Hour < 8) //before 8am
                                return true;
                        }
                        break;
                }

                return false;
            }
        }
    }
}
