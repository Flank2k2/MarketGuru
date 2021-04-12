using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks.Client;
using MarketGuru.Core.Configurations;
using MarketGuru.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace MarketGuru.Core
{

    namespace Microsoft.Extensions.DependencyInjection
    {
        public static class ServiceExtensions
        {
            public static IServiceCollection AddCoreServices(this IServiceCollection services, Action<MarketGuruConfigurations> action = null)
            {   
                services.AddOptions<MarketGuruConfigurations>()
                    .Configure<IConfiguration>((opt, cfg) =>
                    {
                        cfg.GetSection($"{nameof(MarketGuruConfigurations)}").Bind(opt);
                        action?.Invoke(opt);
                    });
                
                services.AddHttpClient();
                services.AddHttpClient("AlphaVantage");

                services.AddScoped<StocksClient>(ctx =>
                {
                    var clientFactory = ctx.GetRequiredService<IHttpClientFactory>();
                    var httpClient = clientFactory.CreateClient("AlphaVantage");
                    var appConfig = ctx.GetRequiredService<IOptions<MarketGuruConfigurations>>()?.Value;
                    if (appConfig == null)
                        throw new Exception("MarketGuruConfigurations config not found");
                    
                    return new AlphaVantageClient(appConfig.ApiKey, httpClient).Stocks();
                });

                services.AddScoped<IStockDataService,StockDataService>();
                services.AddScoped<StockRecommendationService>();

                return services;
            }
        }
    }
}
