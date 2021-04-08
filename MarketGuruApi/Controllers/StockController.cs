using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketGuru.Core.Models;
using MarketGuru.Core.Services;
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
        public string Get(string ticker)
        {
            return "value";
        }

        [HttpGet("{ticker}/")]
        public async Task<ActionResult<StockHistory>> GetHistory(string ticker, DateTime startTime,DateTime endTime)
        {
            
            return Ok();
        }
        
        

    }
}
