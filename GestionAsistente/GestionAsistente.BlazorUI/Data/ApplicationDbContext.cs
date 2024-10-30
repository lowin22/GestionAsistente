using GestionAsistente.AccesoDatos.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionAsistente.BlazorUI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PersonaEF> PersonaEFs { get; set; }
        public DbSet<AsistenteEF> AsistenteEFs { get; set; }
        public DbSet<AdministradorEF> AdministradorEFs { get; set; }
        public DbSet<UsuarioEF> UsuariosEF { get; set; }
        public DbSet<RolEF> RolEFs { get; set; }
        public DbSet<UnidadEF> UnidadEFs { get; set; }
        public DbSet<BadgeEF> BadgesEF { get; set; }
        public DbSet<EstacionTrabajoEF> EstacionTrabajoEFs { get; set; }
        public DbSet<HorarioEF> HorarioEFs { get; set; }
        public DbSet<OficinaEF> OficinaEFs { get; set; }
        public DbSet<HistorialAccionesEF> HistoriaAccionesEFs { get; set; }
        public DbSet<EncargadoEF> EncargadoEFs { get; set; }
        public DbSet<ReporteBadgeEF> ReporteBadgeEFs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Persona)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Unidad)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Encargado)
                .WithMany()
                .HasForeignKey(a => a.EncargadoID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Badge)
                .WithMany()
                .HasForeignKey(a => a.BadgeID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<EncargadoEF>()
                .HasOne(e => e.Unidad)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BadgeEF>()
                .HasOne(b => b.Unidad)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
