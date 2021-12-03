using AutoBogus;
using Frota.Carros.Api;
using Frota.Carros.Api.Responses.Usuario;
using Frota.Carros.Api.ViewModels.Usuario;
using Frota.Carros.Test.Configurations;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Frota.Carros.Test.Integrations.Controllers
{
    public class UsuarioControllerTest : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> _factory;
        protected readonly ITestOutputHelper _output;
        protected readonly HttpClient _httpClient;
        private CadastrarUsuarioViewModel _cadastrarUsuarioViewModel;
        protected TokenResponse _tokenResponse;

        public UsuarioControllerTest(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task CadastrarUsuario_DadosValidos_RetornarSucesso()
        {
            // Arrange
            _cadastrarUsuarioViewModel = new AutoFaker<CadastrarUsuarioViewModel>(AutoBogusConfiguration.LOCATE)
                .RuleFor(x => x.Email, faker => faker.Person.Email);
            StringContent content = new(JsonConvert.SerializeObject(_cadastrarUsuarioViewModel), Encoding.UTF8, "application/json");

            // Act
            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuarios", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task Logar_DadosValidos_RetornarSucesso()
        {
            // Arrange
            LoginViewModel loginViewModel = new(_cadastrarUsuarioViewModel.Email,
               _cadastrarUsuarioViewModel.Senha);
            StringContent content = new(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");

            // Act
            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuarios/auth", content);

            // Assert
            _tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(await httpClientRequest.Content.ReadAsStringAsync());
            
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
            Assert.NotNull(_tokenResponse.Token);
            _output.WriteLine(_tokenResponse.Token);
        }

        public async Task InitializeAsync()
        {
            await CadastrarUsuario_DadosValidos_RetornarSucesso();
            await Logar_DadosValidos_RetornarSucesso();
        }

        public async Task DisposeAsync()
        {
            _httpClient.Dispose();
        }
    }
}
