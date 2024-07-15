using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingRelationshipNamedEntityAndPrescribingInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entidades_nomeadas_por_bula");

            migrationBuilder.AddColumn<Guid>(
                name: "id_bula",
                table: "entidade_nomeada",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_entidade_nomeada_id_bula",
                table: "entidade_nomeada",
                column: "id_bula");

            migrationBuilder.AddForeignKey(
                name: "FK_entidade_nomeada_bulas_id_bula",
                table: "entidade_nomeada",
                column: "id_bula",
                principalTable: "bulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entidade_nomeada_bulas_id_bula",
                table: "entidade_nomeada");

            migrationBuilder.DropIndex(
                name: "IX_entidade_nomeada_id_bula",
                table: "entidade_nomeada");

            migrationBuilder.DropColumn(
                name: "id_bula",
                table: "entidade_nomeada");

            migrationBuilder.CreateTable(
                name: "entidades_nomeadas_por_bula",
                columns: table => new
                {
                    id_bula = table.Column<Guid>(type: "uuid", nullable: false),
                    id_entidade_nomeada = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entidades_nomeadas_por_bula", x => new { x.id_bula, x.id_entidade_nomeada });
                    table.ForeignKey(
                        name: "FK_entidades_nomeadas_por_bula_bulas_id_bula",
                        column: x => x.id_bula,
                        principalTable: "bulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entidades_nomeadas_por_bula_entidade_nomeada_id_entidade_no~",
                        column: x => x.id_entidade_nomeada,
                        principalTable: "entidade_nomeada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entidades_nomeadas_por_bula_id_entidade_nomeada",
                table: "entidades_nomeadas_por_bula",
                column: "id_entidade_nomeada");
        }
    }
}
