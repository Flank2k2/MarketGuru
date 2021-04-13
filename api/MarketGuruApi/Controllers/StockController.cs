using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MarketGuru.Core.Models;
using MarketGuru.Core.Services;
using MarketGuru.Data;
using MarketGuru.Data.Entities;
using MarketGuruApi.Records;
using Microsoft.Extensions.Logging;


namespace MarketGuruApi.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStockDataService _dataService;
        private readonly StockRecommendationService _recommendationService;
        private readonly StockRecommendationRepository _repository;
        public StockController(ILogger<StockController> logger, IStockDataService dataService, StockRecommendationService recommendationService, StockRecommendationRepository repository)
        {
            _logger = logger;
            _dataService = dataService;
            _recommendationService = recommendationService;
            _repository = repository;
        }

        [HttpGet("{ticker}")]
        public async Task<ActionResult<StockResponse>> Get(string ticker, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(ticker))
                return Problem("Invalid stock ticker", statusCode: 400);

            //Sanitize input
            ticker = ticker.Trim().ToUpper();

            //Get data
            var stock = await _dataService.RetrieveStockAsync(ticker);
            if (stock == Stock.UnknownStock)
                return Problem("Stock not found", statusCode: 404);

            var history = await _dataService.RetrieveStockHistoryAsync(ticker);
            var recommendation = _recommendationService.CreateRecommendation(stock, history);
            
           var storageId =  await _repository.SaveStoreRecommendationHistory(new StockRecommendationHistory()
            {
                Recommendation = $"{recommendation.Recommendation}",
                StockTicker = stock.Ticker,
                RecommendationReason = recommendation.Reason,
                Timestamp = DateTime.UtcNow,
                Username = User?.Identity?.Name ?? "Anonymous"
            }, token);
            
            return Ok(new StockResponse(stock, history, recommendation, storageId));
        }
    }
}
