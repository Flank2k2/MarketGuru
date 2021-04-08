using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGuru.Data.Entities
{
    public class StockRecommendationHistory
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string StockTicker { get; set; }
        public string RecommendationReason{get; set;}
        public string Recommendation { get; set; }
    }
}
