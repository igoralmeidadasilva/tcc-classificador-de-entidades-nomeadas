﻿// <auto-generated />
using System;
using Classificador.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    [DbContext(typeof(ClassifierContext))]
    [Migration("20240706224828_NovaMigracao")]
    partial class NovaMigracao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Classificador.Api.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("categorias", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.Classification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comentarios");

                    b.Property<DateTime>("CreatedAtOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<Guid>("IdCategory")
                        .HasColumnType("uuid")
                        .HasColumnName("id_categoria");

                    b.Property<Guid>("IdNamedEntitie")
                        .HasColumnType("uuid")
                        .HasColumnName("id_entidade_nomeada");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdNamedEntitie");

                    b.HasIndex("IdUser");

                    b.ToTable("classificacoes", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.NamedEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("entidade_nomeada", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.NamedEntityPrescribingInformation", b =>
                {
                    b.Property<Guid>("IdPrescribingInformation")
                        .HasColumnType("uuid")
                        .HasColumnName("id_bula");

                    b.Property<Guid>("IdNamedEntity")
                        .HasColumnType("uuid")
                        .HasColumnName("id_entidade_nomeada");

                    b.HasKey("IdPrescribingInformation", "IdNamedEntity");

                    b.HasIndex("IdNamedEntity");

                    b.ToTable("entidades_nomeadas_por_bula", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.PrescribingInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nome");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("texto");

                    b.HasKey("Id");

                    b.ToTable("bulas", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("especialidade", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Contact")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("contato");

                    b.Property<DateTime>("CreatedAtOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("email");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("senha");

                    b.Property<Guid>("IdSpecialty")
                        .HasColumnType("uuid")
                        .HasColumnName("id_especialidade");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nome");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("funcao");

                    b.HasKey("Id");

                    b.HasIndex("IdSpecialty");

                    b.ToTable("usuarios", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.Classification", b =>
                {
                    b.HasOne("Classificador.Api.Domain.Entities.Category", "Category")
                        .WithMany("Classifications")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Classificador.Api.Domain.Entities.NamedEntity", "NamedEntitie")
                        .WithMany("Classifications")
                        .HasForeignKey("IdNamedEntitie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Classificador.Api.Domain.Entities.User", "User")
                        .WithMany("Classifications")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("NamedEntitie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.NamedEntityPrescribingInformation", b =>
                {
                    b.HasOne("Classificador.Api.Domain.Entities.NamedEntity", "NamedEntity")
                        .WithMany("NamedEntityPrescribingsInformation")
                        .HasForeignKey("IdNamedEntity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Classificador.Api.Domain.Entities.PrescribingInformation", "PrescribingInformation")
                        .WithMany("NamedEntityPrescribingsInformation")
                        .HasForeignKey("IdPrescribingInformation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NamedEntity");

                    b.Navigation("PrescribingInformation");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.User", b =>
                {
                    b.HasOne("Classificador.Api.Domain.Entities.Specialty", "Specialty")
                        .WithMany("Users")
                        .HasForeignKey("IdSpecialty")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.Category", b =>
                {
                    b.Navigation("Classifications");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.NamedEntity", b =>
                {
                    b.Navigation("Classifications");

                    b.Navigation("NamedEntityPrescribingsInformation");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.PrescribingInformation", b =>
                {
                    b.Navigation("NamedEntityPrescribingsInformation");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.Specialty", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.User", b =>
                {
                    b.Navigation("Classifications");
                });
#pragma warning restore 612, 618
        }
    }
}
