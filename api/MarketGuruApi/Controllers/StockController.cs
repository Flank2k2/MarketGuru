using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketGuru.Core.Models;
using MarketGuru.Core.Services;
using MarketGuruApi.Reccords;
using Microsoft.Extensions.Logging;


namespace MarketGuruApi.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly StockDataService _dataService;
        private readonly StockRecommendationService _recommendationService;

        public StockController(ILogger<StockController> logger, StockDataService dataService, StockRecommendationService recommendationService)
        {
            _logger = logger;
            _dataService = dataService;
            _recommendationService = recommendationService;
        }

        [HttpGet("{ticker}")]
        public async Task<ActionResult<StockResponse>> Get(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker))
                return Problem("Invalid stock ticker", statusCode: 400);

            var stock = await _dataService.RetrieveStockAsync(ticker);
            if (stock == Stock.UnknownStock)
                return Problem("Stock not found", statusCode: 404);

            var history = await _dataService.RetrieveStockHistoryAsync(ticker);
            var recommendation = _recommendationService.CreateRecommendation(stock, history);

            //TODO: Storage here !! 
            
            return Ok(new StockResponse(stock, history, recommendation));
        }
    }
}
