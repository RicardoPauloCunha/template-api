namespace Frota.Carros.Domain.Models.Usuario
{
    public interface IUsuarioRepository
    {
        Usuario GetById(int usuarioId);
        Usuario GetByEmail(string email);
        void Create(Usuario usuario);
    }
}
