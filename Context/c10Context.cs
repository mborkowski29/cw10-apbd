using cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace cw10.Context;

public class c10Context : DbContext
{

    public c10Context(DbContextOptions<c10Context> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "Mikołaj", LastName = "Kowalski", Birthdate = new DateTime(1990, 1, 1) },
            new Patient { IdPatient = 2, FirstName = "Ewa", LastName = "Nowak", Birthdate = new DateTime(1985, 5, 23) }
        );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com" },
            new Doctor { IdDoctor = 2, FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com" }
        );

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Aspirin", Description = "Pain reliever", Type = "Analgesic" },
            new Medicament { IdMedicament = 2, Name = "Penicillin", Description = "Antibiotic", Type = "Antibiotic" }
        );

        modelBuilder.Entity<Prescription>().HasData(
            new Prescription { IdPrescription = 1, Date = new DateTime(2023, 1, 1), DueDate = new DateTime(2023, 2, 1), IdDoctor = 1, IdPatient = 1 },
            new Prescription { IdPrescription = 2, Date = new DateTime(2023, 1, 10), DueDate = new DateTime(2023, 2, 10), IdDoctor = 2, IdPatient = 2 }
        );

        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 100, Details = "Take one pill every day" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 200, Details = "Take two pills every day" }
        );
    }
}

