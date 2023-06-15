using ExercicioWebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExercicioWebAPI.Map
{
    public class UsuarioMap : BaseMap<Usuario>
    {
        public UsuarioMap() : base("usuario")
        { }

        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Role).HasColumnName("funcao").HasColumnType("varchar(100)").IsRequired();
        }
    }
}
