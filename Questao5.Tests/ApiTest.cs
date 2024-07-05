using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Questao5;
using Microsoft.AspNetCore.Hosting;
using System;
using Questao5.Application;
using Newtonsoft.Json;
using System.Text;

namespace Questao5.Tests
{
    public class ApiTest : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ApiTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost:7140")
            });
        }


        [Fact]
        public async Task Get_Endpoint_ReturnsSuccess()
        {
            // Arrange
            var url = "/api/contacorrente/B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);
        }


        [Fact]
        public async Task Post_Endpoint_ReturnsSuccess()
        {
            // Arrange
            var url = "/api/contacorrente";

            var model = new MovimentarContaRequest
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                TipoMovimento = 'C',
                Valor = 50
            };
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Add("chaveIdempotencia", "5e3354cb-7c39-42d0-b2bd-138de3691a8e");

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);

            var retorno = JsonConvert.DeserializeObject<MovimentarContaResponse>(responseString);
            Assert.NotEmpty(retorno.IdMovimento);
        }
    }
}