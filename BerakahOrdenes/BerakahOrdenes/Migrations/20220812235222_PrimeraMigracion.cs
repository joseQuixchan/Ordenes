using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class PrimeraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioPassHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UsuarioPassSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UsuarioNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioCorreo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioEstado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioFechaSesion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsaurioFechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
