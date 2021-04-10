using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace MarketGuru.Data.Entities
{
    public class StockRecommendationHistory
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
        public string StockTicker { get; set; }
        public string RecommendationReason{get; set;}
        public string Recommendation { get; set; }
        
        public class Converter:IFirestoreConverter<StockRecommendationHistory>
        {
            public object ToFirestore(StockRecommendationHistory value)
            {
                return new Dictionary<string, object>()
                {
                    {"Id" , value.Id},
                    {"Username" , value.Username},
                    {"StockTicker" , value.StockTicker},
                    {"Timestamp" , value.Timestamp.ToUniversalTime()},
                    {"RecommendationReason" , value.RecommendationReason},
                    {"Recommendation",value.Recommendation }
                };
            }

            public StockRecommendationHistory FromFirestore(object value)
            {
                var valueDict = (Dictionary<string, object>)value;
                
                var timestamp = (Google.Cloud.Firestore.Timestamp) valueDict["Timestamp"];
                var stockRecommendationHistory = new StockRecommendationHistory()
                {
                   Timestamp =  timestamp.ToDateTime(),
                   RecommendationReason =(string) valueDict["RecommendationReason"],    
                   Recommendation = (string) valueDict["Recommendation"],
                   StockTicker = (string) valueDict["StockTicker"],
                   Username = (string) valueDict["Username"],
                   Id = (string) valueDict["Id"],

                };

                return stockRecommendationHistory;
            }
        }
    }
}
