using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAsistente.BlazorUI.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialAcciones",
                columns: table => new
                {
                    HistoriaAccionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombrePersona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAcciones", x => x.HistoriaAccionesID);
                });

            migrationBuilder.CreateTable(
                name: "Oficina",
                columns: table => new
                {
                    OficinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oficina", x => x.OficinaID);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.PersonaID);
                });

            migrationBuilder.CreateTable(
                name: "ReporteBadge",
                columns: table => new
                {
                    ReporteBadgeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroBadge = table.Column<int>(type: "int", nullable: false),
                    NombreAsistente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteBadge", x => x.ReporteBadgeID);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Unidad",
                columns: table => new
                {
                    UnidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidad", x => x.UnidadID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstacionTrabajo",
                columns: table => new
                {
                    EstacionTrabajoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    OficinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacionTrabajo", x => x.EstacionTrabajoID);
                    table.ForeignKey(
                        name: "FK_EstacionTrabajo_Oficina_OficinaID",
                        column: x => x.OficinaID,
                        principalTable: "Oficina",
                        principalColumn: "OficinaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    AdministradorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.AdministradorID);
                    table.ForeignKey(
                        name: "FK_Administrador_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrador_Rol_RolID",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrador_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID");
                });

            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    BadgeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Justificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: true),
                    Ocupado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badge", x => x.BadgeID);
                    table.ForeignKey(
                        name: "FK_Badge_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Encargado",
                columns: table => new
                {
                    EncargadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encargado", x => x.EncargadoID);
                    table.ForeignKey(
                        name: "FK_Encargado_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encargado_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: true),
                    UnidadID = table.Column<int>(type: "int", nullable: true),
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuario_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolID",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "RolID");
                    table.ForeignKey(
                        name: "FK_Usuario_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asistente",
                columns: table => new
                {
                    AsistenteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: true),
                    Accesos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncargadoID = table.Column<int>(type: "int", nullable: true),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadgeID = table.Column<int>(type: "int", nullable: true),
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistente", x => x.AsistenteID);
                    table.ForeignKey(
                        name: "FK_Asistente_Badge_BadgeID",
                        column: x => x.BadgeID,
                        principalTable: "Badge",
                        principalColumn: "BadgeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Asistente_Encargado_EncargadoID",
                        column: x => x.EncargadoID,
                        principalTable: "Encargado",
                        principalColumn: "EncargadoID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Asistente_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistente_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    HorarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstacionTrabajoID = table.Column<int>(type: "int", nullable: false),
                    AsistenteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.HorarioID);
                    table.ForeignKey(
                        name: "FK_Horario_Asistente_AsistenteID",
                        column: x => x.AsistenteID,
                        principalTable: "Asistente",
                        principalColumn: "AsistenteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Horario_EstacionTrabajo_EstacionTrabajoID",
                        column: x => x.EstacionTrabajoID,
                        principalTable: "EstacionTrabajo",
                        principalColumn: "EstacionTrabajoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_PersonaID",
                table: "Administrador",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_RolID",
                table: "Administrador",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_UnidadID",
                table: "Administrador",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_BadgeID",
                table: "Asistente",
                column: "BadgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_EncargadoID",
                table: "Asistente",
                column: "EncargadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_PersonaID",
                table: "Asistente",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_UnidadID",
                table: "Asistente",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnidadID",
                table: "AspNetUsers",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Badge_UnidadID",
                table: "Badge",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Encargado_PersonaID",
                table: "Encargado",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Encargado_UnidadID",
                table: "Encargado",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_EstacionTrabajo_OficinaID",
                table: "EstacionTrabajo",
                column: "OficinaID");

            migrationBuilder.CreateIndex(
                name: "IX_Horario_AsistenteID",
                table: "Horario",
                column: "AsistenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Horario_EstacionTrabajoID",
                table: "Horario",
                column: "EstacionTrabajoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PersonaID",
                table: "Usuario",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolID",
                table: "Usuario",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UnidadID",
                table: "Usuario",
                column: "UnidadID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HistorialAcciones");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "ReporteBadge");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Asistente");

            migrationBuilder.DropTable(
                name: "EstacionTrabajo");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropTable(
                name: "Encargado");

            migrationBuilder.DropTable(
                name: "Oficina");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Unidad");
        }
    }
}
