using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class dd6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "UsuarioCambioPass",
                table: "Usuario",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "OrdenDetalle",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "OrdenDetalle");

            migrationBuilder.AlterColumn<bool>(
                name: "UsuarioCambioPass",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
