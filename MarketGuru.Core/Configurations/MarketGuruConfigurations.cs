using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGuru.Core.Configurations
{
   public class MarketGuruConfigurations
    {
        public string ApiKey { get; set; }
        public TimeSpan StockApiCacheConfiguration {get; set;} = TimeSpan.FromHours(5);
        public int MaxVolumeForRecommendation { get; set; } = 10000;
        public int MaxPeriodsForRecommendation { get; set; } = 15;
    }
}
