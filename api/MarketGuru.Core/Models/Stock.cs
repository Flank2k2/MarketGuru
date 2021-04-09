using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGuru.Core.Models
{
    public class Stock
    {
        public static Stock UnknownStock { get; } = new Stock() { Ticker = "Unknown", DisplayName = "Stock not found."};

        public string DisplayName { get; set; }
        public string Ticker { get; set; }
        public decimal DailyHigh {get; set;}
        public decimal DailyLow { get; set; }
    }
}
