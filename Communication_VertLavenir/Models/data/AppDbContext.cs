using Communication_VertLavenir.Models;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventRegistration> EventRegistrations => Set<EventRegistration>();
        public DbSet<Donation> Donations => Set<Donation>();
        public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
        public DbSet<ImpactMetric> ImpactMetrics => Set<ImpactMetric>();
        public DbSet<KeyAction> KeyActions => Set<KeyAction>();
        public DbSet<OrgValue> OrgValues => Set<OrgValue>();
        public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
                b.HasOne(u => u.Address)
                    .WithMany()
                    .HasForeignKey(u => u.AddressId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Event>(b =>
            {
                b.Property(e => e.Category).HasConversion<string>().HasMaxLength(30);
            });

            modelBuilder.Entity<EventRegistration>(b =>
            {
                b.Property(r => r.Status).HasConversion<string>().HasMaxLength(20);
                b.HasOne(r => r.Event)
                    .WithMany(e => e.Registrations)
                    .HasForeignKey(r => r.EventId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(r => r.User)
                    .WithMany(u => u.Registrations)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Donation>(b =>
            {
                b.Property(d => d.Amount).HasPrecision(18, 2);
                b.Property(d => d.PaymentMethod).HasConversion<string>().HasMaxLength(20);
                b.Property(d => d.Status).HasConversion<string>().HasMaxLength(20);
                b.HasOne(d => d.User)
                    .WithMany(u => u.Donations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ContactMessage>(b =>
            {
                b.Property(c => c.Status).HasConversion<string>().HasMaxLength(20);
            });

            modelBuilder.Entity<SiteSetting>(b =>
            {
                b.HasIndex(s => s.Key).IsUnique();
            });

            modelBuilder.Entity<ImpactMetric>().HasIndex(m => m.Key).IsUnique();
        }
    }
}
