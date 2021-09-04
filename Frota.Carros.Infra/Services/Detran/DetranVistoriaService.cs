using Frota.Carros.Domain.Models.Carro;
using Frota.Carros.Domain.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Frota.Carros.Infra.Services.Detran
{
    public class DetranVistoriaService : IVistoriaService
    {
        private readonly DetranOptions _detranOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICarroRepository _carroRepository;

        public DetranVistoriaService(IOptionsMonitor<DetranOptions> optionsMonitor, IHttpClientFactory httpClientFactory, ICarroRepository carroRepository)
        {
            _detranOptions = optionsMonitor.CurrentValue;
            _httpClientFactory = httpClientFactory;
            _carroRepository = carroRepository;
        }

        public async Task AgendarVistoriaCarro(int carroId)
        {
            var veiculo = _carroRepository.GetById(carroId);

            var requestModel = new VistoriaModel()
            {
                Placa = veiculo.Placa,
                AgendadoPara = DateTime.Now.AddDays(_detranOptions.QuantidadeDiasParaAgendamento)
            };

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_detranOptions.BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonContent = JsonSerializer.Serialize(requestModel);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            await client.PostAsync(_detranOptions.VistoriaUrl, contentString);
        }
    }
}
