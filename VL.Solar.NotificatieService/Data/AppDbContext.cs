using Microsoft.EntityFrameworkCore;
using VL.Solar.NotificatieService.Models;

namespace VL.Solar.NotificatieService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() {
        }

        public DbSet<MedewerkerNotificatie> MedewerkerNotificaties { get; set; }
        public DbSet<Notificatie?> Notificaties { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedewerkerNotificatie>()
                .HasKey(mNotificatie => new { mNotificatie.NotificatieId, mNotificatie.MedewerkerId });

            modelBuilder.Entity<Notificatie>()
                .HasKey(notificatie => notificatie.NotificatieId);
            
            modelBuilder.Entity<MedewerkerNotificatie>()
                .HasOne(mNotificatie => mNotificatie.Notificatie)
                .WithMany()
                .HasForeignKey(mNotificatie => mNotificatie.NotificatieId)
                .OnDelete(DeleteBehavior.Restrict);
            

            base.OnModelCreating(modelBuilder);
        }

    }
}