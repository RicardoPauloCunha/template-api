using Frota.Carros.Domain.Models.Carro;
using Frota.Carros.Domain.Models.Usuario;
using Frota.Carros.Infra.EF.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Frota.Carros.Infra.EF
{
    public class FrotaCarrosDbContext : DbContext
    {
        public FrotaCarrosDbContext(DbContextOptions<FrotaCarrosDbContext> options) : base(options)
        {

        }

        public DbSet<Carro> Carro { get; private set; }
        public DbSet<Usuario> Usuario { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarroMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }
    }
}
