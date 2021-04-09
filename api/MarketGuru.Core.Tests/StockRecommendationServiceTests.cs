using System;
using System.Collections.Generic;
using System.Linq;
using MarketGuru.Core.Configurations;
using MarketGuru.Core.Models;
using MarketGuru.Core.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace MarketGuru.Core.Tests
{
    public class StockRecommendationServiceTests
    {
        private static readonly IOptions<MarketGuruConfigurations> DefaultSettings = Options.Create(new MarketGuruConfigurations()
        {
            MaxVolumeForRecommendation = 10000,
            MaxPeriodsForRecommendation = 3
        });



        [Fact]
        public void StockEvaluateProperPeriod()
        {
            //Arrange
            var stock = new Stock() { Ticker = "AAPL" };
            var lst2 = StockGenerator.GenerateHistory(DateTime.Now, Recommendation.Sell,
                volume: DefaultSettings.Value.MaxVolumeForRecommendation, periods: DefaultSettings.Value.MaxPeriodsForRecommendation);
            var lst1 = StockGenerator.GenerateHistory(DateTime.Now.AddMonths(-4), Recommendation.Buy,
                volume: DefaultSettings.Value.MaxVolumeForRecommendation, periods: DefaultSettings.Value.MaxPeriodsForRecommendation);
            var history = new StockHistory(lst1.Concat(lst2));

            //Act
            var services = new StockRecommendationService(new NullLogger<StockRecommendationService>(), DefaultSettings);
            var recommendation = services.CreateRecommendation(stock, history);

            //Assert
            Assert.Equal(Recommendation.Sell, recommendation.Recommendation);
        }

        [Fact]
        public void StockBellowThreshold()
        {
            //Arrange
            var stock = new Stock() { Ticker = "AAPL" };
            var history = new StockHistory(StockGenerator.GenerateHistory(DateTime.Now, Recommendation.Sell, volume: 100));

            //Act
            var services = new StockRecommendationService(new NullLogger<StockRecommendationService>(), DefaultSettings);
            var recommendation = services.CreateRecommendation(stock, history);

            //Assert
            Assert.Equal(Recommendation.Unknown, recommendation.Recommendation);
            Assert.Contains("bellow volume threshold", recommendation.Reason);
        }


        [Fact]
        public void StockBuyRecommendation()
        {
            //Arrange
            var stock = new Stock() { Ticker = "AAPL" };
            var history = new StockHistory(StockGenerator.GenerateHistory(DateTime.Now, Recommendation.Buy));

            //Act
            var services = new StockRecommendationService(new NullLogger<StockRecommendationService>(), DefaultSettings);
            var recommendation = services.CreateRecommendation(stock, history);

            //Assert
            Assert.Equal(Recommendation.Buy, recommendation.Recommendation);
        }

        [Fact]
        public void StockSellRecommendation()
        {
            //Arrange
            var stock = new Stock() { Ticker = "AAPL" };
            var history = new StockHistory(StockGenerator.GenerateHistory(DateTime.Now, Recommendation.Sell));

            //Act
            var services = new StockRecommendationService(new NullLogger<StockRecommendationService>(), DefaultSettings);
            var recommendation = services.CreateRecommendation(stock, history);

            //Assert
            Assert.Equal(Recommendation.Sell, recommendation.Recommendation);
        }

    }
}
