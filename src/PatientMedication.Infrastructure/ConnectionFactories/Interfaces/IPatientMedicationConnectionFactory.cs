using System;
using System.Data;

namespace PatientMedication.Infrastructure.ConnectionFactories.Interfaces
{
    /// <summary>
    /// Interface implemented by factory classes responsible to creating connections to the
    /// PatientMedication database.
    /// </summary>
    public interface IPatientMedicationConnectionFactory
    {
        /// <summary>
        /// Create a connection to the PatientMedication database.
        /// </summary>
        /// <returns>
        /// A new connection to the PatientMedication database.
        /// </returns>
        IDbConnection Create();
    }
}
