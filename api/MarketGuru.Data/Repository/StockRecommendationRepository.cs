using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using MarketGuru.Data.Configurations;
using MarketGuru.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MarketGuru.Data
{
    public interface IStockRecommendationRepository
    {
        Task<string> SaveStoreRecommendationHistory(StockRecommendationHistory model, CancellationToken token = default);
    }

    public sealed class StockRecommendationRepository : IStockRecommendationRepository
    {
        private readonly ILogger _logger;
        private readonly FirestoreConfiguration _settings;
        private readonly FirestoreDb _firestoreDb;
        
        public string CollectionPath => $"{_settings.Database.TrimEnd('/')}/history";
        
        public StockRecommendationRepository(ILogger<StockRecommendationRepository> logger, IOptions<FirestoreConfiguration> settings)
        {
            _logger = logger;
            _settings = settings?.Value;
            _firestoreDb = new FirestoreDbBuilder()
            {
                ProjectId = _settings?.ProjectId,
                ConverterRegistry = new ConverterRegistry
                {
                    new StockRecommendationHistory.Converter(),
                }
            }.Build();
            
        }
        
        public async Task<string> SaveStoreRecommendationHistory(StockRecommendationHistory model, CancellationToken token = default)
        {
            _logger.LogDebug("Storing StockRecommendationRepository: {Stock} (Recommendation: {Recommendation})",model.StockTicker, model.Recommendation);
            var reference = await _firestoreDb.Collection(CollectionPath).AddAsync(model, token);
            return reference.Id;
        }
    }
}
