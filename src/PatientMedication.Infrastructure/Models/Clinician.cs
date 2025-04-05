using System;

namespace PatientMedication.Infrastructure.Models;

public partial class Clinician
{
    public int ClinicianId { get; set; }

    public long ClinicianReference { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public long RegistrationId { get; set; }
}
