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
            MaxPeriodsForRecommendation = 15
        });

        
        
        [Fact]
        public void StockEvaluateProperPeriod()
        {
            //Arrange
            var stock = new Stock() { Ticker = "AAPL" };
            var lst2 = StockGenerator.GenerateHistory(DateTime.Now, StockGenerator.StockTrend.Decrease, volume: 100);
            var lst1 = StockGenerator.GenerateHistory(DateTime.Now.AddDays(-16), StockGenerator.StockTrend.Increase, volume: 100);
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
            var history = new StockHistory(StockGenerator.GenerateHistory(DateTime.Now, StockGenerator.StockTrend.Increase, volume: 100));

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
            var history = new StockHistory(StockGenerator.GenerateHistory(DateTime.Now, StockGenerator.StockTrend.Increase));

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
            var history = new StockHistory(StockGenerator.GenerateHistory(DateTime.Now, StockGenerator.StockTrend.Decrease));

            //Act
            var services = new StockRecommendationService(new NullLogger<StockRecommendationService>(), DefaultSettings);
            var recommendation = services.CreateRecommendation(stock, history);

            //Assert
            Assert.Equal(Recommendation.Sell, recommendation.Recommendation);
        }

    }
}
