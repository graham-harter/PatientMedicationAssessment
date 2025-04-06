using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMedication.Infrastructure.Models;

public partial class MedicationRequest
{
    public int MedicationRequestId { get; set; }

    [ForeignKey("Patient")]
    public long PatientReference { get; set; }

    [ForeignKey("Clinician")]
    public long ClinicianReference { get; set; }

    [ForeignKey("Medication")]
    public string MedicationReference { get; set; } = null!;

    public string ReasonText { get; set; } = null!;

    public DateTime PrescribedDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int FrequencyNumberOfTimes { get; set; }

    public int FrequencyUnitId { get; set; }

    public short MedicationRequestStatusId { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Clinician? Clinician { get; set; }

    public virtual Medication? Medication { get; set; }

    public virtual MedicationRequestStatus? Status { get; set; }

    // Todo: Create navigation property for FrequencyUnit.
}
