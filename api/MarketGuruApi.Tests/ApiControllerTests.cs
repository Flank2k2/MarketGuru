using System;
using System.Net;
using System.Threading.Tasks;
using MarketGuruApi.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MarketGuruApi.Tests
{
    public class ApiControllerTests
        : IClassFixture<MarketGuruTestApplication>
    {
        private readonly MarketGuruTestApplication _factory;

        public ApiControllerTests(MarketGuruTestApplication factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        public async Task Get_GenericApiEndpoints(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Contains("application/json", $"{response.Content.Headers.ContentType}");
        }
        
        [Fact]
        public async Task Get_ErrorEndpoint()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/error");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Contains("application/problem+json", $"{response.Content.Headers.ContentType}");
        }
    }

}
