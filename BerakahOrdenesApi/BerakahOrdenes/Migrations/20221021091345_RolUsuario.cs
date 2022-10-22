using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class RolUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioRolId",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioRolId",
                table: "Usuario",
                column: "UsuarioRolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Rol_UsuarioRolId",
                table: "Usuario",
                column: "UsuarioRolId",
                principalTable: "Rol",
                principalColumn: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Rol_UsuarioRolId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_UsuarioRolId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "UsuarioRolId",
                table: "Usuario");
        }
    }
}
