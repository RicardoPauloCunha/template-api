using AutoBogus;
using Frota.Carros.Api;
using Frota.Carros.Api.ViewModels.Carro;
using Frota.Carros.Domain.Models.Carro;
using Frota.Carros.Test.Configurations;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Frota.Carros.Test.Integrations.Controllers
{
    public class CarroControllerTest : UsuarioControllerTest
    {
        public CarroControllerTest(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task CadastrarCarro_DadosValidosEAutenticado_RetornarSucesso()
        {
            // Arrange
            CadastrarCarroViewModel cadastrarCarroViewModel = new AutoFaker<CadastrarCarroViewModel>(AutoBogusConfiguration.LOCATE);
            StringContent content = new(JsonConvert.SerializeObject(cadastrarCarroViewModel), Encoding.UTF8, "application/json");

            // Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenResponse.Token);
            var httpClientRequest = await _httpClient.PostAsync("api/v1/carros", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task CadastrarCarro_DadosValidosENaoAutenticado_RetornarErro()
        {
            // Arrange
            CadastrarCarroViewModel cadastrarCarroViewModel = new AutoFaker<CadastrarCarroViewModel>(AutoBogusConfiguration.LOCATE);
            StringContent content = new(JsonConvert.SerializeObject(cadastrarCarroViewModel), Encoding.UTF8, "application/json");

            // Act
            var httpClientRequest = await _httpClient.PostAsync("api/v1/carros", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task Listar_NoneEAutenticado_RetornarSucesso()
        {
            // Arrange
            await CadastrarCarro_DadosValidosEAutenticado_RetornarSucesso();

            // Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenResponse.Token);
            var httpClientRequest = await _httpClient.GetAsync("api/v1/carros");

            // Assert
            var response = JsonConvert.DeserializeObject<List<Carro>>(await httpClientRequest.Content.ReadAsStringAsync());
            
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
            Assert.NotEmpty(response);
        }
    }
}
