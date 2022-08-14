using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class TablaModulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulo",
                columns: table => new
                {
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuloNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuloDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuloImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuloEstado = table.Column<bool>(type: "bit", nullable: false),
                    ModuloFechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulo", x => x.ModuloId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modulo");
        }
    }
}
