﻿// <auto-generated />
using System;
using GestionAsistentes.AccesoDatos.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionAsistentes.AccesoDatos.Migrations
{
    [DbContext(typeof(GestionAsistenteContexto))]
    partial class GestionAsistenteContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.AdministradorEF", b =>
                {
                    b.Property<int>("AdministradorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdministradorID"));

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonaID")
                        .HasColumnType("int");

                    b.Property<int>("RolID")
                        .HasColumnType("int");

                    b.Property<int>("UnidadID")
                        .HasColumnType("int");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdministradorID");

                    b.HasIndex("PersonaID");

                    b.HasIndex("RolID");

                    b.HasIndex("UnidadID");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.AsistenteEF", b =>
                {
                    b.Property<int>("AsistenteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AsistenteID"));

                    b.Property<string>("Accesos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BadgeID")
                        .HasColumnType("int");

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EncargadoID")
                        .HasColumnType("int");

                    b.Property<int>("PersonaID")
                        .HasColumnType("int");

                    b.Property<int>("UnidadID")
                        .HasColumnType("int");

                    b.Property<string>("nombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AsistenteID");

                    b.HasIndex("BadgeID");

                    b.HasIndex("EncargadoID");

                    b.HasIndex("PersonaID");

                    b.HasIndex("UnidadID");

                    b.ToTable("Asistente");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.BadgeEF", b =>
                {
                    b.Property<int>("BadgeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BadgeID"));

                    b.Property<string>("Accesos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnidadID")
                        .HasColumnType("int");

                    b.HasKey("BadgeID");

                    b.HasIndex("UnidadID");

                    b.ToTable("Badge");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.EncargadoEF", b =>
                {
                    b.Property<int>("EncargadoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EncargadoID"));

                    b.Property<int>("PersonaID")
                        .HasColumnType("int");

                    b.Property<int>("UnidadID")
                        .HasColumnType("int");

                    b.HasKey("EncargadoID");

                    b.HasIndex("PersonaID");

                    b.HasIndex("UnidadID");

                    b.ToTable("Encargado");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.EstacionTrabajoEF", b =>
                {
                    b.Property<int>("EstacionTrabajoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstacionTrabajoID"));

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("OficinaID")
                        .HasColumnType("int");

                    b.HasKey("EstacionTrabajoID");

                    b.HasIndex("OficinaID");

                    b.ToTable("EstacionTrabajo");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.HistorialAccionesEF", b =>
                {
                    b.Property<int>("HistoriaAccionesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistoriaAccionesID"));

                    b.Property<string>("Accion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombrePersona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoriaAccionesID");

                    b.ToTable("HistorialAcciones");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.HorarioEF", b =>
                {
                    b.Property<int>("HorarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HorarioID"));

                    b.Property<int>("AsistenteID")
                        .HasColumnType("int");

                    b.Property<string>("Dia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstacionTrabajoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("HoraFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.HasKey("HorarioID");

                    b.HasIndex("AsistenteID");

                    b.HasIndex("EstacionTrabajoID");

                    b.ToTable("Horario");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.OficinaEF", b =>
                {
                    b.Property<int>("OficinaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OficinaID"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OficinaID");

                    b.ToTable("Oficina");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.PersonaEF", b =>
                {
                    b.Property<int>("PersonaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonaID"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SegundoApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonaID");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.ReporteBadgeEF", b =>
                {
                    b.Property<int>("ReporteBadgeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReporteBadgeID"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreAsistente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroBadge")
                        .HasColumnType("int");

                    b.HasKey("ReporteBadgeID");

                    b.ToTable("ReporteBadge");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.RolEF", b =>
                {
                    b.Property<int>("RolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolID"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RolID");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.UnidadEF", b =>
                {
                    b.Property<int>("UnidadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnidadID"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnidadID");

                    b.ToTable("Unidad");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.UsuarioEF", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioID"));

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonaID")
                        .HasColumnType("int");

                    b.Property<int>("RolID")
                        .HasColumnType("int");

                    b.Property<int>("UnidadID")
                        .HasColumnType("int");

                    b.HasKey("UsuarioID");

                    b.HasIndex("PersonaID");

                    b.HasIndex("RolID");

                    b.HasIndex("UnidadID");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.AdministradorEF", b =>
                {
                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.PersonaEF", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.RolEF", "Rol")
                        .WithMany()
                        .HasForeignKey("RolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.UnidadEF", "Unidad")
                        .WithMany()
                        .HasForeignKey("UnidadID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");

                    b.Navigation("Rol");

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.AsistenteEF", b =>
                {
                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.BadgeEF", "Badge")
                        .WithMany()
                        .HasForeignKey("BadgeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.EncargadoEF", "Encargado")
                        .WithMany()
                        .HasForeignKey("EncargadoID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.PersonaEF", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.UnidadEF", "Unidad")
                        .WithMany()
                        .HasForeignKey("UnidadID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Badge");

                    b.Navigation("Encargado");

                    b.Navigation("Persona");

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.BadgeEF", b =>
                {
                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.UnidadEF", "Unidad")
                        .WithMany()
                        .HasForeignKey("UnidadID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.EncargadoEF", b =>
                {
                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.PersonaEF", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.UnidadEF", "Unidad")
                        .WithMany()
                        .HasForeignKey("UnidadID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.EstacionTrabajoEF", b =>
                {
                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.OficinaEF", "Oficina")
                        .WithMany()
                        .HasForeignKey("OficinaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Oficina");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.HorarioEF", b =>
                {
                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.AsistenteEF", "Asistente")
                        .WithMany()
                        .HasForeignKey("AsistenteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.EstacionTrabajoEF", "EstacionTrabajo")
                        .WithMany()
                        .HasForeignKey("EstacionTrabajoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asistente");

                    b.Navigation("EstacionTrabajo");
                });

            modelBuilder.Entity("GestionAsistentes.AccesoDatos.Modelos.UsuarioEF", b =>
                {
                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.PersonaEF", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.RolEF", "Rol")
                        .WithMany()
                        .HasForeignKey("RolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAsistentes.AccesoDatos.Modelos.UnidadEF", "Unidad")
                        .WithMany()
                        .HasForeignKey("UnidadID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");

                    b.Navigation("Rol");

                    b.Navigation("Unidad");
                });
#pragma warning restore 612, 618
        }
    }
}
