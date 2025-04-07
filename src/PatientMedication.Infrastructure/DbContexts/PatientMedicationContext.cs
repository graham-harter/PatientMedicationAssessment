using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using PatientMedication.Infrastructure.Configuration.Interfaces;
using PatientMedication.Infrastructure.Models;

namespace PatientMedication.Infrastructure.DbContexts;

public partial class PatientMedicationContext : DbContext
{
    private readonly IPatientMedicationContextConfigurer? _configurer;

    public PatientMedicationContext()
    {
    }

    public PatientMedicationContext(DbContextOptions<PatientMedicationContext> options)
        : base(options)
    {
    }

    public PatientMedicationContext(
        IPatientMedicationContextConfigurer configurer)
    {
        // Validate argument(s).
        if (configurer is null) throw new ArgumentNullException(nameof(configurer));

        // Make these arguments available to the object.
        _configurer = configurer;
    }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Clinician> Clinicians { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<MedicationRequestStatus> MedicationRequestStatuses { get; set; }

    public virtual DbSet<MedicationRequest> MedicationRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // If we have been provided with an IPatientMedicationContextConfigurer instance, use it now.
        if (_configurer is not null && !optionsBuilder.IsConfigured)
        {
            _configurer.ConfigureDbContext(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence("NextPatientReference", "patients")
            .StartsAt(100000000001L)
            .HasMin(100000000001L)
            .HasMax(999999999999L);

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK_Patient");

            entity.ToTable("Patient", "patients");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true);

            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(true);

            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.HasSequence("NextClinicianReference", "clinicians")
            .StartsAt(100000000001L)
            .HasMin(100000000001L)
            .HasMax(999999999999L);

        modelBuilder.Entity<Clinician>(entity =>
        {
            entity.HasKey(e => e.ClinicianId).HasName("PK_Clinician");

            entity.ToTable("Clinician", "clinicians");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true);

            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(true);
        });

        modelBuilder.Entity<MedicationCodeSystem>(entity =>
        {
            entity.HasKey(e => e.MedicationCodeSystemId).HasName("PK_MedicationCodeSystem");

            entity.ToTable("MedicationCodeSystem", "medication");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasData(new[]
            {
                new MedicationCodeSystem(1, "SNOMED"),
            });
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("PK_Medication");

            entity.ToTable("Medication", "medication");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.CodeName)
                .HasMaxLength(100)
                .IsUnicode(true);
        });

        modelBuilder.Entity<MedicationRequestStatus>(entity =>
        {
            entity.HasKey(e => e.MedicationRequestStatusId).HasName("PK_MedicationRequestStatus");

            entity.ToTable("MedicationRequestStatus", "medication");

            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasData(new[]
            {
                new MedicationRequestStatus(1, "Active"),
                new MedicationRequestStatus(2, "On hold"),
                new MedicationRequestStatus(3, "Cancelled"),
                new MedicationRequestStatus(4, "Completed"),
            });
        });

        modelBuilder.Entity<MedicationRequest>(entity =>
        {
            entity.HasKey(e => e.MedicationRequestId).HasName("PK_MedicationRequest");

            entity.ToTable("MedicationRequest", "medication");

            entity.Property(e => e.MedicationReference)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.ReasonText)
                .HasMaxLength(4000)
            .IsUnicode(true);
        });

        modelBuilder.Entity<Patient>()
            .HasMany<MedicationRequest>()
            .WithOne(e => e.Patient)
            .HasForeignKey(e => e.PatientReference)
            .HasPrincipalKey(e => e.PatientReference)
            ;

        modelBuilder.Entity<Clinician>()
            .HasMany<MedicationRequest>()
            .WithOne(e => e.Clinician)
            .HasForeignKey(e => e.ClinicianReference)
            .HasPrincipalKey(e => e.ClinicianReference)
            ;

        modelBuilder.Entity<Medication>()
            .HasMany<MedicationRequest>()
            .WithOne(e => e.Medication)
            .HasForeignKey(e => e.MedicationReference)
            .HasPrincipalKey(e => e.Code)
            ;

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
