using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMedication.Infrastructure.Models;

public partial class Medication
{
    public int MedicationId { get; set; }

    public string Code { get; set; } = null!;

    public string CodeName { get; set; } = null!;

    [ForeignKey("MedicationCodeSystem")]
    public int MedicationCodeSystemId { get; set; }

    public short StrengthValue { get; set; }

    public int StrengthUnitId { get; set; }

    public int FormId { get; set; }

    public virtual MedicationCodeSystem? MedicationCodeSystem { get; set; }

    // Todo: Create navigation properties for StrengthUnit and Form.
}
