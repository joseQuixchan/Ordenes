using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class correccionOrden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientNit",
                table: "Orden",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteCorreo",
                table: "Orden",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteDireccion",
                table: "Orden",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientNit",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "ClienteCorreo",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "ClienteDireccion",
                table: "Orden");
        }
    }
}
