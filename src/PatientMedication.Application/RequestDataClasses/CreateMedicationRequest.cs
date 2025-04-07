using System;
using System.ComponentModel.DataAnnotations;

namespace PatientMedication.Application.RequestDataClasses
{
    /// <summary>
    /// Represents a request to create a medication request.
    /// </summary>
    public sealed class CreateMedicationRequest
    {
        #region Public properties

        [Required]
        public long? PatientReference { get; set; }

        [Required]
        public long? ClinicianReference { get; set; }

        [Required]
        public string MedicationReference { get; set; }

        [Required]
        public string ReasonText { get; set; }

        [Required]
        public DateTime? PrescribedDate { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int? FrequencyNumberOfTimes { get; set; }

        [Required]
        public string? FrequencyUnit { get; set; }

        [Required]
        public string Status { get; set; }

        #endregion // #region Public properties
    }
}
