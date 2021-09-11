using Frota.Carros.Domain.Models.Usuario;

namespace Frota.Carros.Api.Authentication
{
    public interface IAuthenticationService
    {
        string GerarToken(Usuario usuario);
    }
}
