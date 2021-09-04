using Frota.Carros.Authentication;
using Frota.Carros.Configurations.Filters;
using Frota.Carros.Domain.Models.Usuario;
using Frota.Carros.DTOs.ResponseErrors;
using Frota.Carros.ViewModels.Usuario;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Frota.Carros.Controllers
{
    [Route("api/v1/usuarios")]
    [ApiController]
    [ValidationViewModelCustom]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <param name="usuarioInput">Parâmetros</param>
        [HttpPost]
        [SwaggerResponse(statusCode: 201, description: "Usuário cadastrado com sucesso")]
        public IActionResult CadastrarUsuario([FromBody] CadastrarUsuarioViewModel usuarioInput)
        {
            Usuario usuario = new(usuarioInput.Nome, usuarioInput.Email, usuarioInput.Senha);

            _usuarioRepository.Create(usuario);

            return Ok();
        }

        /// <summary>
        /// Efetuar Login
        /// </summary>
        /// <param name="loginInput">Parâmetros</param>
        [HttpPost("auth")]
        [SwaggerResponse(statusCode: 200, description: "Autenticação realizada com sucesso")]
        [SwaggerResponse(statusCode: 401, description: "Email e/ou senha inválido", Type = typeof(ErrorDefault))]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado", Type = typeof(ErrorDefault))]
        public IActionResult Login([FromBody] LoginViewModel loginInput)
        {
            Usuario usuario = _usuarioRepository.GetByEmail(loginInput.Email);

            if (usuario == null)
                return NotFound(new ErrorDefault("Usuário não encontrado"));
            else if (usuario.Senha != usuario.Senha)
                return Unauthorized(new ErrorDefault("Email e/ou senha inválido"));

            var token = _authenticationService.GerarToken(usuario);

            return Ok(new { token });
        }
    }
}
