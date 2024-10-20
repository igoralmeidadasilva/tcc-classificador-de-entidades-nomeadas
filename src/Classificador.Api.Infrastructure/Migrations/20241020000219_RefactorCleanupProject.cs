using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCleanupProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "usuarios",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "usuarios",
                newName: "data_remocao_utc");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "especialidades",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "especialidades",
                newName: "data_remocao_utc");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "entidades_nomeadas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "entidades_nomeadas",
                newName: "data_remocao_utc");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "classificacoes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "classificacoes",
                newName: "data_remocao_utc");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categorias",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "categorias",
                newName: "data_remocao_utc");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "bulas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "bulas",
                newName: "data_remocao_utc");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao_utc",
                table: "usuarios",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao_utc",
                table: "especialidades",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao_utc",
                table: "entidades_nomeadas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao_utc",
                table: "classificacoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao_utc",
                table: "categorias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao_utc",
                table: "bulas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_criacao_utc",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "data_criacao_utc",
                table: "especialidades");

            migrationBuilder.DropColumn(
                name: "data_criacao_utc",
                table: "entidades_nomeadas");

            migrationBuilder.DropColumn(
                name: "data_criacao_utc",
                table: "classificacoes");

            migrationBuilder.DropColumn(
                name: "data_criacao_utc",
                table: "categorias");

            migrationBuilder.DropColumn(
                name: "data_criacao_utc",
                table: "bulas");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_remocao_utc",
                table: "usuarios",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "especialidades",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_remocao_utc",
                table: "especialidades",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "entidades_nomeadas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_remocao_utc",
                table: "entidades_nomeadas",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "classificacoes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_remocao_utc",
                table: "classificacoes",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categorias",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_remocao_utc",
                table: "categorias",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "bulas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_remocao_utc",
                table: "bulas",
                newName: "data_criacao");
        }
    }
}
