using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classificador.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ColumnClassificationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "classificacoes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "classificacoes");
        }
    }
}
