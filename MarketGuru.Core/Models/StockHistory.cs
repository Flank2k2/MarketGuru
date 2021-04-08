using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketGuru.Core.Models
{
    public class StockHistory
    {
        public StockHistory(){}

        public StockHistory(IEnumerable<(DateTime t, decimal value, long volume)> history)
        {
            History = history;
            StartPeriod = history.Min(x => x.t);
            EndPeriod = history.Max(x => x.t);
            Min = history.Min(x => x.value);
            Max = history.Max(x => x.value);
        }
        public DateTime StartPeriod {get; set; }
        public DateTime EndPeriod { get; set; }
        public IEnumerable<(DateTime t, decimal value, long volume)> History { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
    }
}
