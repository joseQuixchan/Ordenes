using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    public partial class TablasProductoyproveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoEstado = table.Column<bool>(type: "bit", nullable: false),
                    ProductoFechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorCorreo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorNit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorDireccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorEstado = table.Column<bool>(type: "bit", nullable: false),
                    ProveedorFechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.ProveedorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Proveedor");
        }
    }
}
