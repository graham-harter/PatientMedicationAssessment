using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMedication.Infrastructure.Models;

public partial class MedicationRequestStatus
{
    #region Constructor(s)

    public MedicationRequestStatus()
    {

    }

    public MedicationRequestStatus(short medicationRequestStatusId, string description)
    {
        // Validate arguments.
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException(nameof(description));

        // Make these arguments available to the object.
        MedicationRequestStatusId = medicationRequestStatusId;
        Description = description;
    }

    #endregion // #region Constructor(s)

    #region Public properties

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public short MedicationRequestStatusId { get; set; }

    public string Description { get; set; } = null!;

    #endregion // #region Public properties
}
