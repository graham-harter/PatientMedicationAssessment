using System;

namespace PatientMedication.Application.RequestDataClasses
{
    /// <summary>
    /// Represents a request to update a medication request.
    /// </summary>
    public sealed class UpdateMedicationRequest
    {
        #region Public properties

        public DateTime? EndDate { get; set; }

        public int? FrequencyNumberOfTimes { get; set; }

        public string? FrequencyUnit { get; set; }

        public string? Status { get; set; }

        #endregion // #region Public properties
    }
}
