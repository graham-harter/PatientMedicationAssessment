﻿using System;
using Microsoft.EntityFrameworkCore;
using PatientMedication.Infrastructure.Models;

namespace PatientMedication.Infrastructure.DbContexts;

public partial class PatientMedicationContext : DbContext
{
    public PatientMedicationContext()
    {
    }

    public PatientMedicationContext(DbContextOptions<PatientMedicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();
    */

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
