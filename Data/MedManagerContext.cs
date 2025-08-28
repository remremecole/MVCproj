using System;
using Microsoft.EntityFrameworkCore;
using MVCproj.Models;

namespace MVCproj.Data
{
    public class MedManagerContext : DbContext
    {
        public MedManagerContext(DbContextOptions<MedManagerContext> options)
            : base(options)
        {
        }

        // DbSet pour chaque table que tu veux gérer
        public DbSet<Patient> Patients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la table patient
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                // Clé primaire
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id_patient")
                      .ValueGeneratedOnAdd();

                // Colonnes
                entity.Property(e => e.Prenom)
                      .HasColumnName("prenom")
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(e => e.Adresse)
                      .HasColumnName("adresse")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Ville)
                      .HasColumnName("ville")
                      .HasColumnType("varchar(100)");

                entity.Property(e => e.DateNaissance)
                      .HasColumnName("date_naissance")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Sexe)
                      .HasColumnName("sexe")
                      .HasColumnType("enum('M','F')")
                      .IsRequired();

                entity.Property(e => e.Poids)
                      .HasColumnName("poids")
                      .HasColumnType("decimal(5,2)");

                entity.Property(e => e.NumeroSecu)
                      .HasColumnName("numero_secu")
                      .HasColumnType("varchar(15)")
                      .IsRequired();

                entity.Property(e => e.DateCreation)
                      .HasColumnName("date_creation")
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DateModification)
                      .HasColumnName("date_modification")
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

                // Index unique sur numero_secu
                entity.HasIndex(e => e.NumeroSecu).IsUnique();
            });
        }

        // Seed data exemple
        public void SeedData()
        {
            if (!Patients.Any())
            {
                Patients.AddRange(
                    new Patient
                    {
                        Prenom = "Jean",
                        DateNaissance = new DateTime(1980, 5, 15),
                        Sexe = "M",
                        NumeroSecu = "123456789"
                    },
                    new Patient
                    {
                        Prenom = "Marie",
                        DateNaissance = new DateTime(1992, 8, 20),
                        Sexe = "F",
                    });
            }
        }
    }
}