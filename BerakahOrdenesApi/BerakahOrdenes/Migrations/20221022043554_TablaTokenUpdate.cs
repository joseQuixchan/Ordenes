using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class TablaTokenUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioUsuario",
                table: "Token");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Token",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Token_UsuarioId",
                table: "Token",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Token_Usuario_UsuarioId",
                table: "Token",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_Usuario_UsuarioId",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Token_UsuarioId",
                table: "Token");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Token");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioUsuario",
                table: "Token",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
