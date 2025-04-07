using System;
using Microsoft.EntityFrameworkCore;
using PatientMedication.Infrastructure.DbContexts;

namespace PatientMedication.Infrastructure.Configuration.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for providing confguration
    /// for <see cref="PatientMedicationContext"/> instances.
    /// </summary>
    public interface IPatientMedicationContextConfigurer
    {
        /// <summary>
        /// Configure an instance of <see cref="PatientMedicationContext"/>
        /// by applying options to the supplied instance of <see cref="DbContextOptionsBuilder"/>.
        /// </summary>
        /// <param name="optionsBuilder">
        /// The instance of <see cref="DbContextOptionsBuilder"/> to use to configure the
        /// instance of <see cref="PatientMedicationContext"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="optionsBuilder"/> argument is <c>null</c>.
        /// </exception>
        void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder);
    }
}
