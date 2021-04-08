using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGuru.Core.Models
{
   public class StockRecommendation
   {
       public Recommendation Recommendation { get; set; } = Recommendation.Unknown;
       public string Reason { get; set; } = "Not Evaluated";

//       public static StockRecommendation EmptyStockRecommendation = new StockRecommendation() {Reason = "No Recommendation", Recommendation = Recommendation.Unknown};
   }

    public enum Recommendation
    {
        Unknown = 0,
        Buy = 1,
        Sell =2,
        Hodl =3
    }
}
