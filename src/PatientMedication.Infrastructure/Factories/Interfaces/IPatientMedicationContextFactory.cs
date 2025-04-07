using System;
using PatientMedication.Infrastructure.DbContexts;

namespace PatientMedication.Infrastructure.Factories.Interfaces
{
    /// <summary>
    /// Interface implemented by factory classes responsible for creating instances of
    /// <see cref="PatientMedicationContext"/>.
    /// </summary>
    public interface IPatientMedicationContextFactory
    {
        /// <summary>
        /// Create a new instance of <see cref="PatientMedicationContext"/>.
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="PatientMedicationContext"/>.
        /// </returns>
        PatientMedicationContext Create();
    }
}
