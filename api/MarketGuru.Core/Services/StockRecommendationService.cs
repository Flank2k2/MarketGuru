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
        private readonly MarketGuruConfigurations _guruConfigurations;
        private readonly ILogger _logger;
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
            var periods = history.History.Take(_guruConfigurations.MaxPeriodsForRecommendation);

            if (periods.Sum(x => x.Volume) < _guruConfigurations.MaxVolumeForRecommendation)
            {
                _logger.LogDebug("Stock: {Ticker} does not reach volume threshold for recommendation");
                return new StockRecommendation()
                {
                    Reason = $"Stock is bellow volume threshold ({_guruConfigurations.MaxVolumeForRecommendation} over {_guruConfigurations.MaxPeriodsForRecommendation} days)",
                    Recommendation = Recommendation.Unknown
                };

            }

            if (periods.First().CLosingPrice > periods.Last().CLosingPrice)
            {
                _logger.LogDebug("Calculate recommendation for stock: {Stock}: {Recommendation}", stock.Ticker, "SELL");
                return new StockRecommendation()
                {
                    Recommendation = Recommendation.Sell,
                    Reason = "Stock is decreasing over period"
                };
            }

            _logger.LogDebug("Calculate recommendation for stock: {Stock}: {Recommendation}", stock.Ticker, "BUY");
            return new StockRecommendation()
            {
                Recommendation = Recommendation.Buy,
                Reason = "Stock is increasing over period"
            };
        }
        
    }
}
