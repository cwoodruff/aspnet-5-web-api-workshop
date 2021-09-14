﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace ChinookASPNETWebAPI.IntegrationTest.API
{
    public class ArtistApiTest
    {
        private readonly HttpClient _client;

        public ArtistApiTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task ArtistGetAllTest(string method)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/Artist/");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", 1)]
        public async Task ArtistGetTest(string method, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/Artist/{id}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}