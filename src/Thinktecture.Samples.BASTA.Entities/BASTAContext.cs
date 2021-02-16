using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Thinktecture.Samples.BASTA.Entities
{
    public class BASTAContext : DbContext
    {
        public BASTAContext(DbContextOptions<BASTAContext> options) :
            base(options)
        {
        }

        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var auditLogs = builder.Entity<AuditLog>();
            var audience = builder.Entity<Audience>();
            var speaker = builder.Entity<Speaker>();
            var session = builder.Entity<Session>();

            auditLogs.ToTable("AuditLogs")
                .HasKey(al => al.Id);

            auditLogs.Property(al => al.Message).IsRequired().HasMaxLength(500);
            auditLogs.Property(al => al.Level).HasConversion<string>();
            auditLogs.Property(al => al.TimeStamp).IsRequired()
                .HasDefaultValueSql("getdate()");
            

            audience.ToTable("Audiences")
                .HasKey(a => a.Id);

            audience.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);


            speaker.ToTable("Speakers")
                .HasKey(s => s.Id);

            speaker.Property(s => s.FirstName).IsRequired().HasMaxLength(75);
            speaker.Property(s => s.LastName).IsRequired().HasMaxLength(75);
            speaker.Property(s => s.Twitter).HasMaxLength(50);
            speaker.Property(s => s.Mail).IsRequired().HasMaxLength(100);
            speaker.Property(s => s.Bio).IsRequired().HasMaxLength(1500);

            session.ToTable("Sessions")
                .HasKey(s => s.Id);

            session.HasOne(s => s.Audience)
                .WithMany(a => a.Sessions)
                .HasForeignKey(s => s.AudienceId);

            session.HasOne(s => s.Speaker)
                .WithMany(sp => sp.Sessions)
                .HasForeignKey(s => s.SpeakerId);

            session.Property(s => s.Title).IsRequired().HasMaxLength(200);
            session.Property(s => s.Description).IsRequired().HasMaxLength(1500);
            session.Property(s => s.Level).IsRequired();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            entities.ToList().ForEach(e =>
            {
                var now = DateTime.UtcNow;
                ((BaseEntity) e.Entity).ModifiedAt = now;
                if (e.State == EntityState.Added)
                {
                    ((BaseEntity) e.Entity).CreatedAt = now;
                }
            });
            return base.SaveChanges();
        }
    }
}
