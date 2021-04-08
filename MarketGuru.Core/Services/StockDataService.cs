using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Stocks.Client;
using MarketGuru.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using MarketGuru.Core.Configurations;
using Microsoft.Extensions.Options;

namespace MarketGuru.Core.Services
{
    public class StockDataService
    {
        private readonly ILogger _logger;
        private readonly StocksClient _client;
        private readonly MarketGuruConfigurations _configurations;
        private readonly IMemoryCache _cache;

        public StockDataService(ILogger logger, StocksClient client, IMemoryCache cache, IOptions<MarketGuruConfigurations> configurationOptions)
        {
            _logger = logger;
            _cache = cache;
            _client = client;
            _configurations = configurationOptions.Value;
        }

        private string GetStockCacheKey(string ticker)
        {
            return $"{nameof(Stock)}:{ticker.ToLower()}";
        }

        private string GetStockHistoryCacheKey(string ticker)
        {
            return $"{nameof(StockHistory)}:{ticker.ToLower()}";
        }

        public async Task<Stock> RetrieveStock(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker)) throw new ArgumentNullException(nameof(ticker));

            if (_cache.TryGetValue<Stock>(GetStockCacheKey(ticker), out var stock))
            {
                _logger.LogDebug("Retrieving stock: {Ticker} from cache", ticker);
                return stock;
            }

            _logger.LogDebug("Retrieving stock: {Ticker} from API", ticker);
            var globalQuote = await _client.GetGlobalQuoteAsync(ticker);
            if (globalQuote == null)
            {
                _logger.LogWarning("Stock: {Ticker} was not found", ticker);
                return Stock.UnknownStock;
            }

            stock = new Stock()
            {
                DailyHigh = globalQuote.HighestPrice,
                DailyLow = globalQuote.LowestPrice,
                Ticker = globalQuote.Symbol
            };

            _cache.Set(GetStockCacheKey(ticker), stock, _configurations.StockApiCacheConfiguration);

            return stock;
        }

        public async Task<StockHistory> RetrieveStockHistory(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker)) throw new ArgumentNullException(nameof(ticker));

            if (_cache.TryGetValue<StockHistory>(GetStockHistoryCacheKey(ticker), out var stockHistory))
            {
                _logger.LogDebug("Retrieving stock history: {Ticker} from cache", ticker);
                return stockHistory;
            }

            _logger.LogDebug("Retrieving stock history: {Ticker} from API", ticker);
            var data = await _client.GetTimeSeriesAsync(ticker, Interval.Daily, OutputSize.Compact, isAdjusted: true);
            if (data == null)
                throw new Exception("Stock not found");

            var sortedData = data.DataPoints.Select(x => (x.Time, x.ClosingPrice, x.Volume))
                                                                        .OrderByDescending(x => x.Time);
            stockHistory = new StockHistory(sortedData);
            
            _cache.Set(GetStockHistoryCacheKey(ticker), stockHistory, _configurations.StockApiCacheConfiguration);
            return stockHistory;
        }
    }
}
