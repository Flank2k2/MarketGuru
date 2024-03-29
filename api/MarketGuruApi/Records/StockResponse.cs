﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketGuru.Core.Models;

namespace MarketGuruApi.Records
{

    public record StockResponse(Stock Stock, StockHistory History, StockRecommendation Recommendation, string Id);
}
