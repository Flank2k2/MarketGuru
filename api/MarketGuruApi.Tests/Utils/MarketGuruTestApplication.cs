using MarketGuru.Core.Microsoft.Extensions.DependencyInjection;
using MarketGuru.Core.Models;
using MarketGuru.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;

namespace MarketGuruApi.Tests.Utils
{
    public class MarketGuruTestApplication : WebApplicationFactory<MarketGuruApi.Startup>
    {
        public static Stock ReferenceStock = new Stock()
        {
            Ticker = "AAPL",
            DisplayName = "Apple Inc",
            DailyHigh = 133.04m,
            DailyLow = 129.47m,
            ClosingPrice = 132.24m
        };
        public static StockHistory ReferenceStockHistory = new StockHistory(new List<StockDataPoint>()
        {
            new StockDataPoint() {ClosingPrice = 125.0m, High = 130.0m, Low = 120.0m, Timestamp = DateTime.UtcNow, Volume = 1000},
            new StockDataPoint() {ClosingPrice = 125.0m, High = 130.0m, Low = 120.0m, Timestamp = DateTime.UtcNow.AddMonths(5), Volume = 1000},
            new StockDataPoint() {ClosingPrice = 125.0m, High = 130.0m, Low = 120.0m, Timestamp = DateTime.UtcNow.AddMonths(-2), Volume = 1000},
            new StockDataPoint() {ClosingPrice = 125.0m, High = 130.0m, Low = 120.0m, Timestamp = DateTime.UtcNow.AddMonths(-3), Volume = 1000},
            new StockDataPoint() {ClosingPrice = 125.0m, High = 130.0m, Low = 120.0m, Timestamp = DateTime.UtcNow.AddMonths(-4), Volume = 1000},
        });

        public static string FirestoreProjectId = "marketguru-data";
        public static string FirestoreEnvironment = $"test{Guid.NewGuid()}";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        { 
            builder.UseEnvironment("Production");
            builder.ConfigureTestServices((services) =>
            {
                services.AddMarketGuruRepository(x =>
                {
                    x.Database = $"environment/{FirestoreEnvironment}";
                });

                //Override service
                services.AddScoped<IStockDataService>(sp =>
                {
                    var dataService = new Mock<IStockDataService>();

                    dataService.Setup(x => x.RetrieveStockAsync("AAPL"))
                        .ReturnsAsync(() => ReferenceStock);
                    
                    dataService.Setup(x => x.RetrieveStockAsync("UNKNOWN"))
                        .ReturnsAsync(() => Stock.UnknownStock);

                    dataService.Setup(x => x.RetrieveStockHistoryAsync(It.IsAny<string>()))
                        .ReturnsAsync(() => ReferenceStockHistory);
                    return dataService.Object;
                });
            });
        }
    }
}
