using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class dd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClienteTelefono",
                table: "Orden",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClienteTelefono",
                table: "Orden");
        }
    }
}
