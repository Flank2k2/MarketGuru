using MarketGuruApi.Reccords;
using MarketGuruApi.Tests.Utils;
using System.Net;
using System.Net.Http.Json;
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
            var cc = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<StockResponse>();
            Assert.Equal(MarketGuruTestApplication.ReferenceStock.Ticker, content.Stock.Ticker);
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
            var content = await response.Content.ReadFromJsonAsync<StockResponse>();
            var storeData = await firestoreClient.RetrieveApiRecommendationById(content.Id);
            
            
        }
        
        [Fact]
        public async Task Get_404_StockEndpoint()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/stock/UNKNOWN");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
