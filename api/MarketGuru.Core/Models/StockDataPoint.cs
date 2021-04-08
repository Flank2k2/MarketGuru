using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGuru.Core.Models
{
    public class StockDataPoint
    {
        public DateTime Timestamp { get; set; } 
        public decimal CLosingPrice { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public long Volume { get; set; }
    }

}
