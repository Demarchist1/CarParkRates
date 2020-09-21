using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkRates.Model
{
    public abstract class BaseRate
    {
        protected BaseRate(string name, DateTime entry, DateTime exit)
        {
            Name = name;
            Entry = entry;
            Exit = exit;
        }

        public string Name { get; set; }
        public DateTime Entry { get; set; }
        public DateTime Exit { get; set; }

        public abstract decimal Rate { get; }
        public abstract bool IsValid { get; }


    }
}
