using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication_VertLavenir.Models;
using Microsoft.EntityFrameworkCore;

namespace Models.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<utilisateur> Utilisateurs { get; set; }
        public DbSet<adresse> Adresses { get; set; }
        public DbSet<evenement> Evenements { get; set; }
        public DbSet<paiement> Paiements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation: utilisateur -> adresse (1-1 or 1-many depending on your needs)
            modelBuilder.Entity<utilisateur>()
                .HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey("AddressId")
                .OnDelete(DeleteBehavior.Cascade);

            // Relation: paiement -> utilisateur (many-to-one)
            modelBuilder.Entity<paiement>()
                .HasOne(p => p.utilisateur)
                .WithMany()
                .HasForeignKey("UtilisateurId")
                .OnDelete(DeleteBehavior.Cascade);

            // Enum conversions (optional but cleaner in DB)
            modelBuilder.Entity<evenement>()
                .Property(e => e.type)
                .HasConversion<string>();

            modelBuilder.Entity<paiement>()
                .Property(p => p.type)
                .HasConversion<string>();

            modelBuilder.Entity<utilisateur>()
                .Property(u => u.role)
                .HasConversion<string>();
        }
    }
}
