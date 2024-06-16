using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HotFixDataBaseColumnsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classificacoes_categorias_id_entidade_nomeada",
                table: "classificacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_classificacoes_entidade_nomeada_IdNamedEntitie",
                table: "classificacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_entidades_nomeadas_por_bula_bulas_IdPrescribingInformation",
                table: "entidades_nomeadas_por_bula");

            migrationBuilder.DropForeignKey(
                name: "FK_entidades_nomeadas_por_bula_entidade_nomeada_IdNamedEntity",
                table: "entidades_nomeadas_por_bula");

            migrationBuilder.RenameColumn(
                name: "IdNamedEntity",
                table: "entidades_nomeadas_por_bula",
                newName: "id_entidade_nomeada");

            migrationBuilder.RenameColumn(
                name: "IdPrescribingInformation",
                table: "entidades_nomeadas_por_bula",
                newName: "id_bula");

            migrationBuilder.RenameIndex(
                name: "IX_entidades_nomeadas_por_bula_IdNamedEntity",
                table: "entidades_nomeadas_por_bula",
                newName: "IX_entidades_nomeadas_por_bula_id_entidade_nomeada");

            migrationBuilder.RenameColumn(
                name: "IdNamedEntitie",
                table: "classificacoes",
                newName: "id_categoria");

            migrationBuilder.RenameIndex(
                name: "IX_classificacoes_IdNamedEntitie",
                table: "classificacoes",
                newName: "IX_classificacoes_id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_classificacoes_categorias_id_categoria",
                table: "classificacoes",
                column: "id_categoria",
                principalTable: "categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classificacoes_entidade_nomeada_id_entidade_nomeada",
                table: "classificacoes",
                column: "id_entidade_nomeada",
                principalTable: "entidade_nomeada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entidades_nomeadas_por_bula_bulas_id_bula",
                table: "entidades_nomeadas_por_bula",
                column: "id_bula",
                principalTable: "bulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entidades_nomeadas_por_bula_entidade_nomeada_id_entidade_no~",
                table: "entidades_nomeadas_por_bula",
                column: "id_entidade_nomeada",
                principalTable: "entidade_nomeada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classificacoes_categorias_id_categoria",
                table: "classificacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_classificacoes_entidade_nomeada_id_entidade_nomeada",
                table: "classificacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_entidades_nomeadas_por_bula_bulas_id_bula",
                table: "entidades_nomeadas_por_bula");

            migrationBuilder.DropForeignKey(
                name: "FK_entidades_nomeadas_por_bula_entidade_nomeada_id_entidade_no~",
                table: "entidades_nomeadas_por_bula");

            migrationBuilder.RenameColumn(
                name: "id_entidade_nomeada",
                table: "entidades_nomeadas_por_bula",
                newName: "IdNamedEntity");

            migrationBuilder.RenameColumn(
                name: "id_bula",
                table: "entidades_nomeadas_por_bula",
                newName: "IdPrescribingInformation");

            migrationBuilder.RenameIndex(
                name: "IX_entidades_nomeadas_por_bula_id_entidade_nomeada",
                table: "entidades_nomeadas_por_bula",
                newName: "IX_entidades_nomeadas_por_bula_IdNamedEntity");

            migrationBuilder.RenameColumn(
                name: "id_categoria",
                table: "classificacoes",
                newName: "IdNamedEntitie");

            migrationBuilder.RenameIndex(
                name: "IX_classificacoes_id_categoria",
                table: "classificacoes",
                newName: "IX_classificacoes_IdNamedEntitie");

            migrationBuilder.AddForeignKey(
                name: "FK_classificacoes_categorias_id_entidade_nomeada",
                table: "classificacoes",
                column: "id_entidade_nomeada",
                principalTable: "categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classificacoes_entidade_nomeada_IdNamedEntitie",
                table: "classificacoes",
                column: "IdNamedEntitie",
                principalTable: "entidade_nomeada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entidades_nomeadas_por_bula_bulas_IdPrescribingInformation",
                table: "entidades_nomeadas_por_bula",
                column: "IdPrescribingInformation",
                principalTable: "bulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entidades_nomeadas_por_bula_entidade_nomeada_IdNamedEntity",
                table: "entidades_nomeadas_por_bula",
                column: "IdNamedEntity",
                principalTable: "entidade_nomeada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
