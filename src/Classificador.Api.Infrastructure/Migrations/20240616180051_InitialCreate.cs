﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bulas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    texto = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foi_deletado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bulas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foi_deletado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entidade_nomeada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foi_deletado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entidade_nomeada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "especialidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foi_deletado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entidades_nomeadas_por_bula",
                columns: table => new
                {
                    IdNamedEntity = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPrescribingInformation = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entidades_nomeadas_por_bula", x => new { x.IdPrescribingInformation, x.IdNamedEntity });
                    table.ForeignKey(
                        name: "FK_entidades_nomeadas_por_bula_bulas_IdPrescribingInformation",
                        column: x => x.IdPrescribingInformation,
                        principalTable: "bulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entidades_nomeadas_por_bula_entidade_nomeada_IdNamedEntity",
                        column: x => x.IdNamedEntity,
                        principalTable: "entidade_nomeada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    senha = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    nome = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    contato = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    funcao = table.Column<string>(type: "text", nullable: false),
                    id_especialidade = table.Column<Guid>(type: "uuid", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foi_deletado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_especialidade_id_especialidade",
                        column: x => x.id_especialidade,
                        principalTable: "especialidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "classificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentarios = table.Column<string>(type: "text", nullable: true),
                    IdNamedEntitie = table.Column<Guid>(type: "uuid", nullable: false),
                    id_entidade_nomeada = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foi_deletado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_classificacoes_categorias_id_entidade_nomeada",
                        column: x => x.id_entidade_nomeada,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_classificacoes_entidade_nomeada_IdNamedEntitie",
                        column: x => x.IdNamedEntitie,
                        principalTable: "entidade_nomeada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_classificacoes_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_classificacoes_IdNamedEntitie",
                table: "classificacoes",
                column: "IdNamedEntitie");

            migrationBuilder.CreateIndex(
                name: "IX_classificacoes_id_entidade_nomeada",
                table: "classificacoes",
                column: "id_entidade_nomeada");

            migrationBuilder.CreateIndex(
                name: "IX_classificacoes_id_usuario",
                table: "classificacoes",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_entidades_nomeadas_por_bula_IdNamedEntity",
                table: "entidades_nomeadas_por_bula",
                column: "IdNamedEntity");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_especialidade",
                table: "usuarios",
                column: "id_especialidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classificacoes");

            migrationBuilder.DropTable(
                name: "entidades_nomeadas_por_bula");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "bulas");

            migrationBuilder.DropTable(
                name: "entidade_nomeada");

            migrationBuilder.DropTable(
                name: "especialidade");
        }
    }
}
