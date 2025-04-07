namespace PatientMedication.Application.ReturnDataClasses
{
    /// <summary>
    /// Data class representing a medication request.
    /// </summary>
    public sealed class MedicationRequest
    {
        #region Constructor(s)

        public MedicationRequest()
        {

        }

        public MedicationRequest(
            int medicationRequestId, string medicationCodeName, string clinicianFirstName, string clinicianSurname)
        {
            // Validate arguments.
            if (string.IsNullOrWhiteSpace(medicationCodeName)) throw new ArgumentNullException(nameof(medicationCodeName));
            if (string.IsNullOrWhiteSpace(clinicianFirstName)) throw new ArgumentNullException(nameof(clinicianFirstName));
            if (string.IsNullOrWhiteSpace(clinicianSurname)) throw new ArgumentNullException(nameof(clinicianSurname));

            // Make these arguments available to the object.
            MedicationRequestId = medicationRequestId;
            MedicationCodeName = medicationCodeName.Trim();
            ClinicianFirstName = clinicianFirstName.Trim();
            ClinicianSurname = clinicianSurname.Trim();
        }

        #endregion // #region Constructor(s)

        #region Public properties

        public int MedicationRequestId { get; set; }

        public string MedicationCodeName { get; set; }

        public string ClinicianFirstName { get; set; }

        public string ClinicianSurname { get; set; }

        #endregion // #region Public properties
    }
}
