using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketGuru.Core.Configurations;
using MarketGuru.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MarketGuru.Core.Services
{
    public class StockRecommendationService
    {
        private readonly ILogger _logger;
        private readonly MarketGuruConfigurations _guruConfigurations;

        public StockRecommendationService(ILogger<StockRecommendationService> logger, IOptions<MarketGuruConfigurations> appConfiguration)
        {
            _guruConfigurations = appConfiguration?.Value;
            _logger = logger;
        }
        
        public StockRecommendation CreateRecommendation(Stock stock, StockHistory history)
        {
            if (stock == null) throw new ArgumentNullException(nameof(stock));
            if (history == null) throw new ArgumentNullException(nameof(history));

            _logger.LogDebug("Calculate recommendation for stock: {Ticker} over the last {MaxPeriodsForRecommendation} days", stock.Ticker, _guruConfigurations.MaxPeriodsForRecommendation);

            //Sorting the collection since i am not going to make assumptions...
            //Really this should be implement with proper collection and IComparable but for the moment we are dealing with n<1000 
            var sortedHistory = history.History.OrderBy(x => x.Timestamp);
            var periods = sortedHistory.Take(_guruConfigurations.MaxPeriodsForRecommendation);
            var initialStockData = periods.First();
            var finalStockData = periods.Last();
            var totalVolume = periods.Sum(x => x.Volume);
            var periodLengthInDays = (finalStockData.Timestamp - initialStockData.Timestamp).TotalDays;
            var priceDifference = finalStockData.ClosingPrice - initialStockData.ClosingPrice;
            
            if (totalVolume < _guruConfigurations.MaxVolumeForRecommendation)
            {
                _logger.LogDebug("Stock: {Ticker} does not reach volume threshold for recommendation");
                return new StockRecommendation()
                {
                    Reason = $"Stock is bellow volume threshold ({_guruConfigurations.MaxVolumeForRecommendation} over {periodLengthInDays} days)",
                    Recommendation = Recommendation.Unknown
                };

            }
            
            if (priceDifference < _guruConfigurations.SellThreshold)
            {
                _logger.LogDebug("Calculate recommendation for stock: {Stock}: {Recommendation}", stock.Ticker, "SELL");
                return new StockRecommendation()
                {
                    Recommendation = Recommendation.Sell,
                    Reason = $"Stock has lost ${priceDifference} over {periodLengthInDays} days"
                };
            }

            _logger.LogDebug("Calculate recommendation for stock: {Stock}: {Recommendation}", stock.Ticker, "BUY");
            return new StockRecommendation()
            {
                Recommendation = Recommendation.Buy,
                Reason = $"Stock has gained ${priceDifference} over {periodLengthInDays} days"
            };
        }

    }
}
