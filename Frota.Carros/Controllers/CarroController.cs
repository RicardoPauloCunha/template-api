using Frota.Carros.Configurations.Filters;
using Frota.Carros.Domain.Models.Carro;
using Frota.Carros.Domain.Services;
using Frota.Carros.DTOs.ResponseErrors;
using Frota.Carros.ViewModels.Carro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Frota.Carros.Controllers
{
    [Route("api/v1/carros")]
    [ApiController]
    [Authorize]
    [SwaggerResponse(statusCode: 401, description: "Você não tem autorização para realizar essa solicitação", Type = null)]
    [ValidationViewModelCustom]
    public class CarroController : ControllerBase
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IVistoriaService _vistoriaService;

        public CarroController(ICarroRepository carroRepository, IVistoriaService vistoriaService)
        {
            _carroRepository = carroRepository;
            _vistoriaService = vistoriaService;
        }

        /// <summary>
        /// Listar carros
        /// </summary>
        [HttpGet]
        [SwaggerResponse(statusCode: 200, description: "Lista de carros")]
        public IActionResult Get()
        {
            return Ok(_carroRepository.GetAll());
        }

        [HttpGet("{carroId}")]
        [SwaggerResponse(statusCode: 200, description: "Carro encontrado")]
        [SwaggerResponse(statusCode: 404, description: "Carro não encontrado", Type = typeof(ErrorDefault))]
        public IActionResult Get([FromRoute] int carroId)
        {
            Carro carro = _carroRepository.GetById(carroId);

            if (carro == null)
                return NotFound(new ErrorDefault("Carro não encontrado"));

            return Ok(carro);
        }

        /// <summary>
        /// Cadastrar carro
        /// </summary>
        /// <param name="carroInput">Parâmetros</param>
        [HttpPost]
        [SwaggerResponse(statusCode: 201, description: "Carro cadastrado com sucesso")]
        public IActionResult Post([FromBody] CadastrarCarroViewModel carroInput)
        {
            Carro carro = new(carroInput.Placa, carroInput.Marca, carroInput.AnoFabricacao);

            _carroRepository.Create(carro);

            return CreatedAtAction(nameof(Get), new { id = carro.Id }, carro);
        }

        /// <summary>
        /// Atualiza os dados do carro
        /// </summary>
        /// <param name="carroInput">Parâmetros</param>
        [HttpPut]
        [SwaggerResponse(statusCode: 204, description: "Carro atualizado com sucesso")]
        [SwaggerResponse(statusCode: 404, description: "Carro não encontrado", Type = typeof(ErrorDefault))]
        public IActionResult Put([FromBody] AtualizarCarroViewModel carroInput)
        {
            Carro carro = _carroRepository.GetById(carroInput.Id);

            if (carro == null)
                return NotFound(new ErrorDefault("Carro não encontrado"));

            carro.AlterarPlaca(carroInput.Placa);
            carro.AlterarMarca(carroInput.Marca);
            carro.AlterarAnoFabricacao(carroInput.AnoFabricacao);

            _carroRepository.Update(carro);

            return NoContent();
        }

        [HttpDelete("{carroId}")]
        [SwaggerResponse(statusCode: 204, description: "Carro deletado com sucesso")]
        [SwaggerResponse(statusCode: 404, description: "Carro não encontrado", Type = typeof(ErrorDefault))]
        public IActionResult Delete([FromRoute] int carroId)
        {
            Carro carro = _carroRepository.GetById(carroId);

            if (carro == null)
                return NotFound(new ErrorDefault("Carro não encontrado"));

            _carroRepository.Delete(carro);

            return NoContent();
        }

        [HttpPut("{carroId}/vistoria")]
        [SwaggerResponse(statusCode: 204, description: "Agendamento de vistoria realizado com sucesso")]
        public async Task<IActionResult> Put(int carroId)
        {
            await _vistoriaService.AgendarVistoriaCarro(carroId);

            return NoContent();
        }
    }
}
