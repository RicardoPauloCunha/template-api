using Frota.Carros.Domain.Models.Carro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Frota.Carros.Infra.EF.Mappings
{
    public class CarroMapping : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.ToTable("TB_Carro");
            builder.HasKey(x => x.Id);
        }
    }
}
