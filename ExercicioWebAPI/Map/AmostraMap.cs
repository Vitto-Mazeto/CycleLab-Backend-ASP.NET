using ExercicioWebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExercicioWebAPI.Map
{
    public class AmostraMap : BaseMap<Amostra>
    {
        public AmostraMap() : base("amostra")
        { }

        public override void Configure(EntityTypeBuilder<Amostra> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.NumeroDeExames).HasColumnName("numero_de_exames").HasColumnType("int").IsRequired();
        }
    }
}
