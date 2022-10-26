﻿// <auto-generated />
using System;
using BerakahOrdenes.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BerakahOrdenes.Migrations
{
    [DbContext(typeof(DBOrdenes))]
    [Migration("20221026104124_pepe3")]
    partial class pepe3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BerakahOrdenes.Modelos.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"), 1L, 1);

                    b.Property<string>("ClienteApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteCorreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteDireccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ClienteEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ClienteFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ClienteNit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteTelefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"), 1L, 1);

                    b.Property<string>("MenuDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MenuEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("MenuFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("MenuImagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModuloId")
                        .HasColumnType("int");

                    b.HasKey("MenuId");

                    b.HasIndex("ModuloId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Modulo", b =>
                {
                    b.Property<int>("ModuloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuloId"), 1L, 1);

                    b.Property<string>("ModuloDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ModuloEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModuloFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModuloImagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModuloNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModuloId");

                    b.ToTable("Modulo");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Orden", b =>
                {
                    b.Property<int>("OrdenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrdenId"), 1L, 1);

                    b.Property<string>("ClienteCorreo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteDireccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteNit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteTelefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OrdenEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrdenFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrdenFechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UsuarioNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrdenId");

                    b.ToTable("Orden");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.OrdenDetalle", b =>
                {
                    b.Property<int>("OrdenDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrdenDetalleId"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OrdenDetalleEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrdenDetalleFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrdenId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUniario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrdenDetalleId");

                    b.HasIndex("OrdenId");

                    b.ToTable("OrdenDetalle");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"), 1L, 1);

                    b.Property<string>("ProductoDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductoEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ProductoFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductoNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ProductoPrecio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductoId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Proveedor", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProveedorId"), 1L, 1);

                    b.Property<string>("ProveedorCorreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProveedorDireccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProveedorEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ProveedorFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProveedorNit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProveedorNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProveedorTelefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolId"), 1L, 1);

                    b.Property<string>("RolDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RolEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RolFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("RolNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RolId");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.RolMenu", b =>
                {
                    b.Property<int>("RolMenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolMenuId"), 1L, 1);

                    b.Property<bool>("Agregar")
                        .HasColumnType("bit");

                    b.Property<bool>("Consultar")
                        .HasColumnType("bit");

                    b.Property<bool>("Eliminar")
                        .HasColumnType("bit");

                    b.Property<bool>("Imprimir")
                        .HasColumnType("bit");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<bool>("Modificar")
                        .HasColumnType("bit");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<bool>("RolMenuEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RolMenuFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("RolMenuId");

                    b.HasIndex("MenuId");

                    b.HasIndex("RolId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("RolMenu");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenId"), 1L, 1);

                    b.Property<string>("CodigoSeguridad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TokenEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TokenFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("TokenId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"), 1L, 1);

                    b.Property<DateTime>("UsaurioFechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("UsuarioApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("UsuarioCambioPass")
                        .HasColumnType("bit");

                    b.Property<string>("UsuarioCorreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UsuarioEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UsuarioFechaCambioPass")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UsuarioFechaSesion")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioIntentos")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("UsuarioPassHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("UsuarioPassHashAnterior")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("UsuarioPassSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("UsuarioPassSaltAnterior")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("UsuarioRolId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioTelefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioUsuarioId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.HasIndex("UsuarioRolId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.UsuarioRol", b =>
                {
                    b.Property<int>("UsuarioRolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioRolId"), 1L, 1);

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<bool>("UsuarioRolEstado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UsuarioRolFechaCreacion")
                        .HasColumnType("datetime2");

                    b.HasKey("UsuarioRolId");

                    b.HasIndex("RolId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioRol");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Menu", b =>
                {
                    b.HasOne("BerakahOrdenes.Modelos.Modulo", "Modulo")
                        .WithMany("Menus")
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modulo");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.OrdenDetalle", b =>
                {
                    b.HasOne("BerakahOrdenes.Modelos.Orden", "Orden")
                        .WithMany("OrdenDetalles")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orden");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.RolMenu", b =>
                {
                    b.HasOne("BerakahOrdenes.Modelos.Menu", "Menu")
                        .WithMany("RolMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerakahOrdenes.Modelos.Rol", "Rol")
                        .WithMany("RolMenus")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerakahOrdenes.Modelos.Usuario", "Usuario")
                        .WithMany("RolMenus")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Token", b =>
                {
                    b.HasOne("BerakahOrdenes.Modelos.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Usuario", b =>
                {
                    b.HasOne("BerakahOrdenes.Modelos.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("UsuarioRolId");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.UsuarioRol", b =>
                {
                    b.HasOne("BerakahOrdenes.Modelos.Rol", "Rol")
                        .WithMany("UsuarioRols")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerakahOrdenes.Modelos.Usuario", "Usuario")
                        .WithMany("UsuarioRols")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Menu", b =>
                {
                    b.Navigation("RolMenus");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Modulo", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Orden", b =>
                {
                    b.Navigation("OrdenDetalles");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Rol", b =>
                {
                    b.Navigation("RolMenus");

                    b.Navigation("UsuarioRols");
                });

            modelBuilder.Entity("BerakahOrdenes.Modelos.Usuario", b =>
                {
                    b.Navigation("RolMenus");

                    b.Navigation("UsuarioRols");
                });
#pragma warning restore 612, 618
        }
    }
}
