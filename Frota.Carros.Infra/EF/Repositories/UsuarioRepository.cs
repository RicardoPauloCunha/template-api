using Frota.Carros.Domain.Models.Usuario;
using System.Linq;

namespace Frota.Carros.Infra.EF.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FrotaCarrosDbContext _context;

        public UsuarioRepository(FrotaCarrosDbContext context)
        {
            _context = context;
        }

        public void Create(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario GetByEmail(string email)
        {
            return _context
                .Usuario
                .FirstOrDefault(x => x.Email == email);
        }

        public Usuario GetById(int usuarioId)
        {
            return _context
                .Usuario
                .FirstOrDefault(x => x.Id == usuarioId);
        }
    }
}
