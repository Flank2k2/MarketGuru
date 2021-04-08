using MarketGuru.Core.Models;
using System;
using System.Collections.Generic;

namespace MarketGuru.Core.Tests
{
    public static class StockGenerator
    {
        public enum StockTrend
        {
            Increase,
            Decrease
        }

        //We should really be using historical data that is KNOWN to be correct.
        //This should be verified extensively too ! 
        public static IEnumerable<StockDataPoint> GenerateHistory(DateTime startDate, StockTrend trend, int volume = 100000, int periods = 15, int min = 100, int max = 300)
        {
            var rand = new Random();
            var list = new List<StockDataPoint>();
            var volumePerDay = volume / periods;
            var leftOverVolume = volume % periods;

            for (int i = 1; i < (periods - 1); i++)
            {
                var value = rand.Next(min, max);
                list.Add(new StockDataPoint() { Timestamp = startDate.AddDays(-1 * i), CLosingPrice = value + 0.001m, Volume = volumePerDay, Low = 0, High = 10000 });
            }

            //Add start and end values             
            if (trend == StockTrend.Increase)
            {
                list.Add(new StockDataPoint() { Timestamp = startDate.AddDays(0), CLosingPrice = min, Volume = volumePerDay + leftOverVolume, Low = 0, High = 10000});
                list.Add(new StockDataPoint() { Timestamp = startDate.AddDays(-1 * (periods - 1)), CLosingPrice = max, Volume = volumePerDay, Low = 0, High = 10000 });
            }
            else
            {
                list.Add(new StockDataPoint() { Timestamp = startDate.AddDays(0), CLosingPrice = max, Volume = volumePerDay + leftOverVolume, Low = 0, High = 10000 });
                list.Add(new StockDataPoint() { Timestamp = startDate.AddDays(-1 * (periods - 1)), CLosingPrice = min, Volume = volumePerDay, Low = 0, High = 10000 });
            }

            return list;
        }


    }
}
