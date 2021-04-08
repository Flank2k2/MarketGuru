using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketGuru.Core.Models
{
    public class StockHistory
    {
        public StockHistory(IEnumerable<StockDataPoint> history)
        {
            History = history;
            StartPeriod = history.Min(x => x.Timestamp);
            EndPeriod = history.Max(x => x.Timestamp);
            Low = history.Min(x => x.CLosingPrice);
            High = history.Max(x => x.CLosingPrice);
        }
        public DateTime StartPeriod {get; set; }
        public DateTime EndPeriod { get; set; }
        public IEnumerable<StockDataPoint> History { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
    }
}
