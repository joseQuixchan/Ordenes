using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class dd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientNit",
                table: "Orden",
                newName: "ClienteNit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClienteNit",
                table: "Orden",
                newName: "ClientNit");
        }
    }
}
