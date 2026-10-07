using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Rola)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasOne<Club>()
                .WithMany()
                .HasForeignKey(u => u.ClubId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(p => p.Club)
                .WithMany()
                .HasForeignKey(p => p.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Parents)
                .WithMany()
                .UsingEntity(j => j.ToTable("PlayerParents"));
        }
    }

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.HasOne(t => t.Club)
                .WithMany()
                .HasForeignKey(t => t.ClubId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TeamCoachConfiguration : IEntityTypeConfiguration<TeamCoach>
    {
        public void Configure(EntityTypeBuilder<TeamCoach> builder)
        {
            // klucz złożony - TeamCoach nie ma własnego Id
            builder.HasKey(tc => new { tc.TeamId, tc.CoachId });

            builder.HasOne(tc => tc.Team)
                .WithMany(t => t.TeamCoaches)
                .HasForeignKey(tc => tc.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tc => tc.Coach)
                .WithMany()
                .HasForeignKey(tc => tc.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(tm => tm.Id);

            builder.HasOne(tm => tm.Team)
                .WithMany(t => t.TeamMembers)
                .HasForeignKey(tm => tm.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tm => tm.Player)
                .WithMany(p => p.TeamMembers)
                .HasForeignKey(tm => tm.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // nie unikalny, bo zawodnik może wrócić do drużyny
            builder.HasIndex(tm => new { tm.TeamId, tm.PlayerId });
        }
    }

    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Location)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.HasOne(t => t.Club)
                .WithMany()
                .HasForeignKey(t => t.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Team)
                .WithMany(team => team.Trainings)
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Creator)
                .WithMany()
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.TeamId, t.StartTime });
        }
    }

    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.HasOne(a => a.Training)
                .WithMany(t => t.Attendances)
                .HasForeignKey(a => a.TrainingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Player)
                .WithMany(p => p.Attendances)
                .HasForeignKey(a => a.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Marker)
                .WithMany()
                .HasForeignKey(a => a.MarkedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => new { a.TrainingId, a.PlayerId })
                .IsUnique();
        }
    }

    public class AbsenceConfiguration : IEntityTypeConfiguration<Absence>
    {
        public void Configure(EntityTypeBuilder<Absence> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Reason)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(a => a.Training)
                .WithMany(t => t.Absences)
                .HasForeignKey(a => a.TrainingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Player)
                .WithMany(p => p.Absences)
                .HasForeignKey(a => a.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Reporter)
                .WithMany()
                .HasForeignKey(a => a.ReportedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => new { a.TrainingId, a.PlayerId })
                .IsUnique();
        }
    }

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                .HasPrecision(18, 2);

            builder.Property(p => p.Period)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.HasOne(p => p.Player)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Club)
                .WithMany()
                .HasForeignKey(p => p.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => new { p.PlayerId, p.Period });
        }
    }

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Content)
                .IsRequired()
                .HasMaxLength(4000);

            builder.HasOne(m => m.Club)
                .WithMany()
                .HasForeignKey(m => m.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Type)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(n => new { n.UserId, n.ReadAt });
        }
    }

    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.EntityId)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(a => a.Club)
                .WithMany()
                .HasForeignKey(a => a.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserId jest nullable - po usunięciu użytkownika log zostaje
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(a => new { a.ClubId, a.CreatedAt });
        }
    }
}