using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; } 
        public DbSet<Notification> Notifications { get; set; } 
        public DbSet<TeamMember> TeamMembers { get; set; } 
        public DbSet<Team> Teams { get; set; } 
        public DbSet<Message> Messages { get; set; }
        public DbSet<TeamCoach> TeamCoaches { get; set; }
        public DbSet<Training> Trainings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TeamCoach>()
                .HasKey(tc => new { tc.TeamId, tc.CoachId });
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        }
}
