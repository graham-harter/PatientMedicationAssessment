using System;
using System.Data;
using System.Data.SqlClient;
using PatientMedication.Infrastructure.Configuration.Interfaces;
using PatientMedication.Infrastructure.ConnectionFactories.Interfaces;

namespace PatientMedication.Infrastructure.ConnectionFactories
{
    /// <summary>
    /// Factory classes responsible to creating connections to the PatientMedication database.
    /// </summary>
    internal sealed class PatientMedicationConnectionFactory : IPatientMedicationConnectionFactory
    {
        private readonly IPatientMedicationConnectionString _connectionString;

        public PatientMedicationConnectionFactory(IPatientMedicationConnectionString connectionString)
        {
            // Validate argument(s).
            if (connectionString is null) throw new ArgumentNullException(nameof(connectionString));

            // Make these arguments available to the object.
            _connectionString = connectionString;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(_connectionString.Get());
        }
    }
}
