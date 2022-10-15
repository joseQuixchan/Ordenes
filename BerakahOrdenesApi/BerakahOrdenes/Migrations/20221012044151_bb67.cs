using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class bb67 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "UsuarioPassHashAnterior",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "UsuarioPassSaltAnterior",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioPassHashAnterior",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "UsuarioPassSaltAnterior",
                table: "Usuario");
        }
    }
}
