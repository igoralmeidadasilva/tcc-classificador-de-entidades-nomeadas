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
    [Migration("20240817170445_ColumnClassification.Status")]
    partial class ColumnClassificationStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
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

                    b.Property<Guid>("IdNamedEntity")
                        .HasColumnType("uuid")
                        .HasColumnName("id_entidade_nomeada");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdNamedEntity");

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

                    b.Property<Guid?>("IdPrescribingInformation")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("id_bula");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("foi_deletado");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.HasIndex("IdPrescribingInformation");

                    b.ToTable("entidades_nomeadas", (string)null);
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

                    b.ToTable("especialidades", (string)null);
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Contact")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
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

                    b.Property<Guid?>("IdSpecialty")
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

                    b.HasIndex("Email")
                        .IsUnique();

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

                    b.HasOne("Classificador.Api.Domain.Entities.NamedEntity", "NamedEntity")
                        .WithMany("Classifications")
                        .HasForeignKey("IdNamedEntity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Classificador.Api.Domain.Entities.User", "User")
                        .WithMany("Classifications")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("NamedEntity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.NamedEntity", b =>
                {
                    b.HasOne("Classificador.Api.Domain.Entities.PrescribingInformation", "PrescribingInformation")
                        .WithMany("NamedEntities")
                        .HasForeignKey("IdPrescribingInformation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Classificador.Api.Domain.ValueObjects.NamedEntity.WordPosition", "WordPosition", b1 =>
                        {
                            b1.Property<Guid>("NamedEntityId")
                                .HasColumnType("uuid");

                            b1.Property<int>("EndPosition")
                                .HasColumnType("integer")
                                .HasColumnName("posicao_final");

                            b1.Property<int>("StartPosition")
                                .HasColumnType("integer")
                                .HasColumnName("posicao_inicial");

                            b1.HasKey("NamedEntityId");

                            b1.ToTable("entidades_nomeadas");

                            b1.WithOwner()
                                .HasForeignKey("NamedEntityId");
                        });

                    b.Navigation("PrescribingInformation");

                    b.Navigation("WordPosition")
                        .IsRequired();
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
                });

            modelBuilder.Entity("Classificador.Api.Domain.Entities.PrescribingInformation", b =>
                {
                    b.Navigation("NamedEntities");
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
