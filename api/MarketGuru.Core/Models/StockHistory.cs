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
            
        }
        public DateTime StartPeriod {get; set; }
        public DateTime EndPeriod { get; set; }
        public IEnumerable<StockDataPoint> History { get; set; }
      
    }
}
