using MarketGuruApi.Records;
using MarketGuruApi.Tests.Utils;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MarketGuruApi.Tests
{
    public class StockControllerTests
        : IClassFixture<MarketGuruTestApplication>
    {
        private readonly MarketGuruTestApplication _factory;

        public StockControllerTests(MarketGuruTestApplication factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_200_StockEndpoint()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/stock/{MarketGuruTestApplication.ReferenceStock.Ticker}");

            //Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var stockResponse = JsonConvert.DeserializeObject<StockResponse>(content);
            Assert.Equal(MarketGuruTestApplication.ReferenceStock.Ticker, stockResponse.Stock.Ticker);
        }
        
        [Fact]
        public async Task Get_200_StockEndpoint_HistorySaved()
        {
            // Arrange
            var firestoreClient = new DataRepositoryClient();
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/stock/{MarketGuruTestApplication.ReferenceStock.Ticker}");

            //Assert
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var stockResponse = JsonConvert.DeserializeObject<StockResponse>(content);
            var data = await firestoreClient.RetrieveApiRecommendationById(stockResponse.Id);
            
            Assert.Equal(data["Recommendation"], stockResponse.Recommendation.Recommendation.ToString());
        }
        
        [Fact]
        public async Task Get_404_StockEndpoint()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/stock/{MarketGuruTestApplication.UnknownStock}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
