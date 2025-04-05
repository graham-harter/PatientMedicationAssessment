using System;
using PatientMedication.Infrastructure.Configuration.Interfaces;

namespace PatientMedication.Infrastructure.Configuration
{
    /// <summary>
    /// Class responsible for providing the connection string to the PatientMedication database.
    /// </summary>
    internal sealed class PatientMedicationConnectionString : IPatientMedicationConnectionString
    {
        private readonly string _connectionString;

        public PatientMedicationConnectionString(
            string connectionString)
        {
            // Validate argument(s).
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            // Make these arguments available to the object.
            _connectionString = connectionString;
        }

        public string Get()
        {
            return _connectionString;
        }
    }
}
