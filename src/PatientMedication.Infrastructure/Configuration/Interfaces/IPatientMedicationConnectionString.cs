namespace PatientMedication.Infrastructure.Configuration.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for providing the connection string
    /// to the PatientMedication database.
    /// </summary>
    public interface IPatientMedicationConnectionString
    {
        /// <summary>
        /// Get the connection string.
        /// </summary>
        /// <returns>
        /// The connection string to the PatientMedication database.
        /// </returns>
        string Get();
    }
}
