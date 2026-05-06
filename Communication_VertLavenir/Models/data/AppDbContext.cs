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

            // Seed Adresses
            modelBuilder.Entity<adresse>().HasData(
                new adresse { id = 1, numAdresse = "1", nomRue = "Rue du Stade", NumApt = null },
                new adresse { id = 2, numAdresse = "2", nomRue = "Avenue des Fleurs", NumApt = "A" },
                new adresse { id = 3, numAdresse = "3", nomRue = "Boulevard Central", NumApt = null },
                new adresse { id = 4, numAdresse = "4", nomRue = "Rue Sainte", NumApt = "12B" },
                new adresse { id = 5, numAdresse = "5", nomRue = "Rue des Rosiers", NumApt = null },
                new adresse { id = 6, numAdresse = "6", nomRue = "Place de la République", NumApt = null }
            );

            // Seed Utilisateurs
            // Note: l'enum role défini dans Models contient : admin, chefBenevol, benevol, donneur, utilisateur
            // Il n'y a que 4 rôles hors 'admin' ; pour obtenir 5 utilisateurs non-admin un rôle est répété.
            modelBuilder.Entity<utilisateur>().HasData(
                new utilisateur
                {
                    id = 1,
                    nom = "Dupont",
                    prenom = "Alice",
                    role = role.chefBenevol,
                    email = "alice.dupont@example.com",
                    password = "Pass123!",
                    numero = "0600000001",
                    AddressId = 1
                },
                new utilisateur
                {
                    id = 2,
                    nom = "Martin",
                    prenom = "Bob",
                    role = role.benevol,
                    email = "bob.martin@example.com",
                    password = "Pass123!",
                    numero = "0600000002",
                    AddressId = 2
                },
                new utilisateur
                {
                    id = 3,
                    nom = "Leroy",
                    prenom = "Claire",
                    role = role.donneur,
                    email = "claire.leroy@example.com",
                    password = "Pass123!",
                    numero = "0600000003",
                    AddressId = 3
                },
                new utilisateur
                {
                    id = 4,
                    nom = "Petit",
                    prenom = "David",
                    role = role.utilisateur,
                    email = "david.petit@example.com",
                    password = "Pass123!",
                    numero = "0600000004",
                    AddressId = 4
                },
                new utilisateur
                {
                    id = 5,
                    nom = "Moreau",
                    prenom = "Emma",
                    role = role.chefBenevol, // rôle répété car seulement 4 rôles non-admin disponibles
                    email = "emma.moreau@example.com",
                    password = "Pass123!",
                    numero = "0600000005",
                    AddressId = 5
                },
                new utilisateur
                {
                    id = 6,
                    nom = "Admin",
                    prenom = "Super",
                    role = role.admin,
                    email = "admin@example.com",
                    password = "AdminPass!23",
                    numero = "0600000000",
                    AddressId = 6
                }
            );
        }
    }
}
