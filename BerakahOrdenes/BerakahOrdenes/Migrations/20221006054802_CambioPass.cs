using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class CambioPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UsuarioCambioPass",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioCambioPass",
                table: "Usuario");
        }
    }
}
