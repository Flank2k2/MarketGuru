using System;
using System.Collections.Generic;
using System.Text;
using MarketGuru.Core.Models;

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
        //This should be tested extensively too ! 
        public static IEnumerable<(DateTime t, decimal value, long volume)> GenerateHistory(DateTime startDate, StockTrend trend, int volume = 100000, int periods = 15, int min = 100, int max = 300)
        {
            var rand = new Random();
            var list = new List<(DateTime t, decimal value, long volume)>();
            var volumePerDay = volume / periods;
            var leftOverVolume = volume % periods;

            for (int i = 1; i < (periods - 1); i++)
            {
                var value = rand.Next(min, max);
                list.Add((startDate.AddDays(-1 * i), value + 0.001m, volumePerDay));
            }

            //Add start and end values             
            if (trend == StockTrend.Increase)
            {
                list.Add((startDate.AddDays(0), min, volumePerDay + leftOverVolume));
                list.Add((startDate.AddDays(-1 * (periods - 1)), max, volumePerDay));
            }
            else
            {
                list.Add((startDate.AddDays(0), max, volumePerDay + leftOverVolume));
                list.Add((startDate.AddDays(-1 * (periods - 1)), min, volumePerDay));
            }

            return list;
        }


    }
}
