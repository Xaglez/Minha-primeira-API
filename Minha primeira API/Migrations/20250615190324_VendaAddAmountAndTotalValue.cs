using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minha_primeira_API.Migrations
{
    /// <inheritdoc />
    public partial class VendaAddAmountAndTotalValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Vendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Vendas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Vendas");
        }
    }
}
