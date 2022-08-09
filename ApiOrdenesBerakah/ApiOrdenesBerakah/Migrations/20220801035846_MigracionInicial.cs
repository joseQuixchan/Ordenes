using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiOrdenesBerakah.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(nullable: true),
                    userPassHash = table.Column<byte[]>(nullable: true),
                    userPassSalt = table.Column<byte[]>(nullable: true),
                    nombre = table.Column<string>(nullable: true),
                    apellido = table.Column<string>(nullable: true),
                    correo = table.Column<string>(nullable: true),
                    telefono = table.Column<string>(nullable: true),
                    genero = table.Column<int>(nullable: false),
                    fechaIngreso = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idUsuario);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
