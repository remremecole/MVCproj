using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCproj.Models;


namespace MVCproj.Data
{
public class MedManagerContext : IdentityDbContext
{
public MedManagerContext(DbContextOptions<MedManagerContext> options) : base(options) { }


public DbSet<Patient> Patients => Set<Patient>();
public DbSet<Medicament> Medicaments => Set<Medicament>();
public DbSet<Ordonnance> Ordonnances => Set<Ordonnance>();
public DbSet<Allergie> Allergies => Set<Allergie>();
public DbSet<Antecedent> Antecedents => Set<Antecedent>();
public DbSet<Contient> Contients => Set<Contient>();
public DbSet<Souffre> Souffres => Set<Souffre>();
public DbSet<Possede> Possedes => Set<Possede>();
public DbSet<Incompatible> Incompatibles => Set<Incompatible>();
public DbSet<ContreIndique> ContreIndiques => Set<ContreIndique>();


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
base.OnModelCreating(modelBuilder);


modelBuilder.Entity<Patient>(entity =>
{
entity.ToTable("patient");
entity.HasKey(e => e.Id);
entity.Property(e => e.Id).HasColumnName("id_patient").ValueGeneratedOnAdd();
entity.Property(e => e.Prenom).HasColumnName("prenom").HasMaxLength(50).IsRequired();
entity.Property(e => e.Adresse).HasColumnName("adresse").HasMaxLength(255);
entity.Property(e => e.Ville).HasColumnName("ville").HasMaxLength(100);
entity.Property(e => e.DateNaissance).HasColumnName("date_naissance").HasColumnType("date").IsRequired();
entity.Property(e => e.Sexe).HasColumnName("sexe").HasMaxLength(1).IsRequired();
entity.Property(e => e.Poids).HasColumnName("poids").HasColumnType("decimal(5,2)");
entity.Property(e => e.NumeroSecu).HasColumnName("numero_secu").HasMaxLength(15).IsRequired();
entity.Property(e => e.DateCreation).HasColumnName("date_creation").HasColumnType("datetime");
entity.Property(e => e.DateModification).HasColumnName("date_modification").HasColumnType("datetime");
entity.HasIndex(e => e.NumeroSecu).IsUnique();
});


            modelBuilder.Entity<Medicament>(e =>
            {
                e.ToTable("medicament");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("id_medicament").ValueGeneratedOnAdd();
                e.Property(x => x.Nom).HasColumnName("nom").HasMaxLength(255).IsRequired();
                e.Property(x => x.Quantite).HasColumnName("quantite").HasMaxLength(50);
                e.Property(x => x.Posologie).HasColumnName("posologie").HasMaxLength(255);
                e.Property(x => x.Composition).HasColumnName("composition");
                e.Property(x => x.Categorie).HasColumnName("categorie").HasMaxLength(100);
                e.Property(x => x.DateCreation).HasColumnName("date_creation").HasColumnType("datetime");
                e.Property(x => x.DateModification).HasColumnName("date_modification").HasColumnType("datetime");
            });
modelBuilder.Entity<Allergie>(e =>
{
e.ToTable("allergie");
e.HasKey(x => x.Id);
e.Property(x => x.Id).HasColumnName("id_allergie").ValueGeneratedOnAdd();
e.Property(x => x.Nom).HasColumnName("nom").HasMaxLength(100).IsRequired();
e.Property(x => x.Description).HasColumnName("description");
e.Property(x => x.DateCreation).HasColumnName("date_creation").HasColumnType("datetime");
});


modelBuilder.Entity<Antecedent>(e =>
{
e.ToTable("antecedent");
e.HasKey(x => x.Id);
e.Property(x => x.Id).HasColumnName("id_antecedent").ValueGeneratedOnAdd();
e.Property(x => x.Nom).HasColumnName("nom").HasMaxLength(100).IsRequired();
e.Property(x => x.Description).HasColumnName("description");
e.Property(x => x.DateCreation).HasColumnName("date_creation").HasColumnType("datetime");
});


            modelBuilder.Entity<Ordonnance>(e =>
            {
                e.ToTable("ordonnance");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("id_ordonnance").ValueGeneratedOnAdd();
                e.Property(x => x.DateDebut).HasColumnName("date_debut").HasColumnType("date").IsRequired();
                e.Property(x => x.DateFin).HasColumnName("date_fin").HasColumnType("date").IsRequired();
                e.Property(x => x.RefComplementaire).HasColumnName("ref_complementaire").HasMaxLength(100);
                e.Property(x => x.Pdf).HasColumnName("pdf");
                e.Property(x => x.IdMedecin).HasColumnName("id_medecin");
                e.Property(x => x.IdPatient).HasColumnName("id_patient");
                e.Property(x => x.DateCreation).HasColumnName("date_creation").HasColumnType("datetime");
                e.Property(x => x.DateModification).HasColumnName("date_modification").HasColumnType("datetime");


                e.HasOne(x => x.Patient)
    .WithMany()
    .HasForeignKey(x => x.IdPatient)
    .OnDelete(DeleteBehavior.Cascade);
            });
modelBuilder.Entity<Contient>(e =>
{
e.ToTable("contient");
e.HasKey(x => new { x.IdOrdonnance, x.IdMedicament });
e.Property(x => x.IdOrdonnance).HasColumnName("id_ordonnance");
e.Property(x => x.IdMedicament).HasColumnName("id_medicament");
e.Property(x => x.QuantitePrescrite).HasColumnName("quantite_prescrite").HasMaxLength(50).IsRequired();
e.Property(x => x.PosologiePrescrite).HasColumnName("posologie_prescrite").HasMaxLength(255).IsRequired();


e.HasOne(x => x.Ordonnance)
.WithMany(x => x.Contenus)
.HasForeignKey(x => x.IdOrdonnance)
.OnDelete(DeleteBehavior.Cascade);


e.HasOne(x => x.Medicament)
.WithMany()
.HasForeignKey(x => x.IdMedicament)
.OnDelete(DeleteBehavior.Cascade);
});


modelBuilder.Entity<Souffre>(e =>
{
e.ToTable("souffre");
e.HasKey(x => new { x.IdPatient, x.IdAllergie });
e.Property(x => x.IdPatient).HasColumnName("id_patient");
e.Property(x => x.IdAllergie).HasColumnName("id_allergie");
e.Property(x => x.DateDiagnostic).HasColumnName("date_diagnostic").HasColumnType("date");
e.Property(x => x.Severite).HasColumnName("severite").HasMaxLength(20);
});


            modelBuilder.Entity<Possede>(e =>
            {
                e.ToTable("possede");
                e.HasKey(x => new { x.IdPatient, x.IdAntecedent });
                e.Property(x => x.IdPatient).HasColumnName("id_patient");
                e.Property(x => x.IdAntecedent).HasColumnName("id_antecedent");
                e.Property(x => x.DateDiagnostic).HasColumnName("date_diagnostic").HasColumnType("date");
                e.Property(x => x.Description).HasColumnName("description");
            });
modelBuilder.Entity<Incompatible>(e =>
{
e.ToTable("incompatible");
e.HasKey(x => new { x.IdMedicament, x.IdAllergie });
e.Property(x => x.IdMedicament).HasColumnName("id_medicament");
e.Property(x => x.IdAllergie).HasColumnName("id_allergie");
e.Property(x => x.NiveauRisque).HasColumnName("niveau_risque").HasMaxLength(20);
e.Property(x => x.Description).HasColumnName("description");
});


modelBuilder.Entity<ContreIndique>(e =>
{
e.ToTable("contre_indique");
e.HasKey(x => new { x.IdMedicament, x.IdAntecedent });
e.Property(x => x.IdMedicament).HasColumnName("id_medicament");
e.Property(x => x.IdAntecedent).HasColumnName("id_antecedent");
e.Property(x => x.NiveauRisque).HasColumnName("niveau_risque").HasMaxLength(20);
e.Property(x => x.Description).HasColumnName("description");
});
}
}
}