﻿// <auto-generated />
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations.ExcWebAPI
{
    [DbContext(typeof(ExcWebAPIContext))]
    [Migration("20230627172635_Exame")]
    partial class Exame
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("Domain.Entities.Amostra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.Property<int>("NumeroDeExames")
                        .HasColumnType("int")
                        .HasColumnName("numero_de_exames");

                    b.HasKey("Id");

                    b.ToTable("amostra", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Exame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmostraId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resultado")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AmostraId");

                    b.ToTable("Exames");
                });

            modelBuilder.Entity("Domain.Entities.Exame", b =>
                {
                    b.HasOne("Domain.Entities.Amostra", "Amostra")
                        .WithMany("Exames")
                        .HasForeignKey("AmostraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amostra");
                });

            modelBuilder.Entity("Domain.Entities.Amostra", b =>
                {
                    b.Navigation("Exames");
                });
#pragma warning restore 612, 618
        }
    }
}