using System;
using System.Collections.Generic;

namespace PatientMedication.Infrastructure.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public long PatientReference { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Sex { get; set; } = null!;
}
