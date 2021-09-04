using Frota.Carros.Domain.Models.Usuario;

namespace Frota.Carros.Authentication
{
    public interface IAuthenticationService
    {
        string GerarToken(Usuario usuario);
    }
}
