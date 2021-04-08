using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGuru.Data.Entities
{
    public class StockHistory
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string StockTicker { get; set; }
        public string ResponseType{get; set;}
        public string Response { get; set; }
    }
}
