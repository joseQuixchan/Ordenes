using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class bb10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoPrecioUnitario",
                table: "Producto");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductoPrecio",
                table: "Producto",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoPrecio",
                table: "Producto");

            migrationBuilder.AddColumn<string>(
                name: "ProductoPrecioUnitario",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
