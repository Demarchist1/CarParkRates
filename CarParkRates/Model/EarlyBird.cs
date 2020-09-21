using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkRates.Model
{
    public class EarlyBirdRate : BaseRate
    {
        public EarlyBirdRate(DateTime entry, DateTime exit)
            : base("Early Bird", entry, exit) { }

        public override decimal Rate
        {
            get { return 13; }
        }

        public override bool IsValid
        {
            get
            {
                if (Entry.Hour >= 6 && Entry.Hour < 9)
                {
                    var adjustedExit = Exit.Subtract(new TimeSpan(0, 30, 0));
                    if (adjustedExit.Hour >= 15 && adjustedExit.Hour < 23)
                        return true;
                }
                return false;
            }
        }
    }
}
