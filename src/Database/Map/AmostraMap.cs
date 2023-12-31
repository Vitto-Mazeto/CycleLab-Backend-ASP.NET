﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Map
{
    public class AmostraMap : BaseMap<Amostra>
    {
        public AmostraMap() : base("amostra")
        { }

        public override void Configure(EntityTypeBuilder<Amostra> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome).HasColumnName("Nome").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.NumeroDeRegistro).HasColumnName("NumeroDeRegistro").HasColumnType("int").IsRequired();
        }
    }
}
