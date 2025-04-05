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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
