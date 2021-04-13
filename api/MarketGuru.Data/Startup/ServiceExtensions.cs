using MarketGuru.Data.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using MarketGuru.Data;

namespace MarketGuru.Core
{

    namespace Microsoft.Extensions.DependencyInjection
    {
        public static class ServiceExtensions
        {
            public static IServiceCollection AddMarketGuruRepository(this IServiceCollection services, Action<FirestoreConfiguration> action = null)
            {   
                services.AddOptions<FirestoreConfiguration>()
                    .Configure<IConfiguration>((opt, cfg) =>
                    {
                        cfg.GetSection($"{nameof(FirestoreConfiguration)}").Bind(opt);
                        action?.Invoke(opt);
                    });

                services.AddScoped<IStockRecommendationRepository,StockRecommendationRepository>();

                return services;
            }
        }
    }
}
