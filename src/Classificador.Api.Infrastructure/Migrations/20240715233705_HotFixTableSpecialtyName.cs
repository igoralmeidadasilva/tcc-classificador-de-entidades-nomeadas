using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HotFixTableSpecialtyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_especialidade_id_especialidade",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_especialidade",
                table: "especialidade");

            migrationBuilder.RenameTable(
                name: "especialidade",
                newName: "especialidades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_especialidades",
                table: "especialidades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_especialidades_id_especialidade",
                table: "usuarios",
                column: "id_especialidade",
                principalTable: "especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_especialidades_id_especialidade",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_especialidades",
                table: "especialidades");

            migrationBuilder.RenameTable(
                name: "especialidades",
                newName: "especialidade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_especialidade",
                table: "especialidade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_especialidade_id_especialidade",
                table: "usuarios",
                column: "id_especialidade",
                principalTable: "especialidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
