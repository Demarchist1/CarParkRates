using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParkRates.Model
{
    public class RateCalculator
    {
        public static RateResult Calculate(DateTime entry, DateTime exit)
        {
            var allRates = new List<BaseRate>()
            {
                new EarlyBirdRate(entry,exit),
                new NightRate(entry,exit),
                new WeekendRate(entry,exit),
                new StandardRate(entry,exit)
            };

            var best = allRates
                .Where(r => r.IsValid)
                .OrderBy(r => r.Rate)
                .First();

            return new RateResult(best);
        }

    }
}
