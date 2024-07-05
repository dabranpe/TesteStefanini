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
using Questao5.Infrastructure;
using Questao5.Domain.Entities;

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
        public async Task ObterSaldo_Sucesso()
        {
            // Arrange
            string idConta = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var url = $"/api/contacorrente/{idConta}";            

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);

            var retorno = JsonConvert.DeserializeObject<ConsultarSaldoContaResponse>(responseString);
            Assert.NotNull(retorno);            
        }

        [Fact]
        public async Task ObterSaldo_ContaInativa()
        {
            // Arrange
            string idConta = "D2E02051-7067-ED11-94C0-835DFA4A16C9";
            var url = $"/api/contacorrente/{idConta}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
            
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);

            var retorno = JsonConvert.DeserializeObject<CustomResponse>(responseString);
            Assert.NotNull(retorno);
            Assert.NotNull(retorno.messages);
        }


        [Fact]
        public async Task MovimentarConta_Sucesso()
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

            _client.DefaultRequestHeaders.Add("chaveIdempotencia", Guid.NewGuid().ToString());

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);

            var retorno = JsonConvert.DeserializeObject<MovimentarContaResponse>(responseString);
            Assert.NotNull(retorno);
            Assert.NotEmpty(retorno.IdMovimento);
        }

        [Fact]
        public async Task MovimentarConta_Idempotencia()
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

            var chaveIdempotencia = Guid.NewGuid().ToString();
            _client.DefaultRequestHeaders.Add("chaveIdempotencia", chaveIdempotencia);

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);

            var retorno = JsonConvert.DeserializeObject<MovimentarContaResponse>(responseString);
            Assert.NotNull(retorno);
            Assert.NotEmpty(retorno.IdMovimento);


            var idMovimento = retorno.IdMovimento;

            // Act 2
            response = await _client.PostAsync(url, content);

            // Assert 2
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.OK);
            responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);

            retorno = JsonConvert.DeserializeObject<MovimentarContaResponse>(responseString);
            Assert.NotNull(retorno);
            Assert.NotEmpty(retorno.IdMovimento);

            Assert.Equal(retorno.IdMovimento, idMovimento);
        }
    }
}