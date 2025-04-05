using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMedication.Infrastructure.Models;

public partial class MedicationCodeSystem
{
    #region Constructor(s)

    public MedicationCodeSystem()
    {

    }

    public MedicationCodeSystem(int medicationCodeSystemId, string code)
    {
        // Validate arguments.
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentNullException(nameof(code));

        // Make these arguments available to the object.
        MedicationCodeSystemId = medicationCodeSystemId;
        Code = code;
    }

    #endregion // #region Constructor(s)

    #region Public properties

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int MedicationCodeSystemId { get; set; }

    public string Code { get; set; } = null!;

    #endregion // #region Public properties
}
