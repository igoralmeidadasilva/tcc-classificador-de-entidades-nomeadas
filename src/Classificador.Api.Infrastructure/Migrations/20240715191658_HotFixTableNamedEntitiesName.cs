using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HotFixTableNamedEntitiesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classificacoes_entidade_nomeada_id_entidade_nomeada",
                table: "classificacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_entidade_nomeada_bulas_id_bula",
                table: "entidade_nomeada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_entidade_nomeada",
                table: "entidade_nomeada");

            migrationBuilder.RenameTable(
                name: "entidade_nomeada",
                newName: "entidades_nomeadas");

            migrationBuilder.RenameIndex(
                name: "IX_entidade_nomeada_id_bula",
                table: "entidades_nomeadas",
                newName: "IX_entidades_nomeadas_id_bula");

            migrationBuilder.AddPrimaryKey(
                name: "PK_entidades_nomeadas",
                table: "entidades_nomeadas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_classificacoes_entidades_nomeadas_id_entidade_nomeada",
                table: "classificacoes",
                column: "id_entidade_nomeada",
                principalTable: "entidades_nomeadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entidades_nomeadas_bulas_id_bula",
                table: "entidades_nomeadas",
                column: "id_bula",
                principalTable: "bulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classificacoes_entidades_nomeadas_id_entidade_nomeada",
                table: "classificacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_entidades_nomeadas_bulas_id_bula",
                table: "entidades_nomeadas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_entidades_nomeadas",
                table: "entidades_nomeadas");

            migrationBuilder.RenameTable(
                name: "entidades_nomeadas",
                newName: "entidade_nomeada");

            migrationBuilder.RenameIndex(
                name: "IX_entidades_nomeadas_id_bula",
                table: "entidade_nomeada",
                newName: "IX_entidade_nomeada_id_bula");

            migrationBuilder.AddPrimaryKey(
                name: "PK_entidade_nomeada",
                table: "entidade_nomeada",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_classificacoes_entidade_nomeada_id_entidade_nomeada",
                table: "classificacoes",
                column: "id_entidade_nomeada",
                principalTable: "entidade_nomeada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entidade_nomeada_bulas_id_bula",
                table: "entidade_nomeada",
                column: "id_bula",
                principalTable: "bulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
