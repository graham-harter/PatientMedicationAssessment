using System;
using Microsoft.EntityFrameworkCore;
using PatientMedication.Infrastructure.Configuration.Interfaces;
using PatientMedication.Infrastructure.DbContexts;

namespace PatientMedication.Infrastructure.Configuration
{
    /// <summary>
    /// Class responsible for providing configuration for <see cref="PatientMedicationContext"/> instances.
    /// </summary>
    internal sealed class PatientMedicationContextConfigurer : IPatientMedicationContextConfigurer
    {
        private readonly IPatientMedicationConnectionString _connectionString;

        #region Constructor(s)

        public PatientMedicationContextConfigurer(
            IPatientMedicationConnectionString connectionString)
        {
            // Validate argument(s).
            if (connectionString is null) throw new ArgumentNullException(nameof(connectionString));

            // Make these arguments available to the object.
            _connectionString = connectionString;
        }

        #endregion // #region Constructor(s)

        #region Public methods

        public void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder)
        {
            // Validate argument(s).
            if (optionsBuilder is null) throw new ArgumentNullException(nameof(optionsBuilder));

            optionsBuilder.UseSqlServer(
                _connectionString.Get(),
                builder => builder.MigrationsAssembly("PatientMedication.Infrastructure"));
        }

        #endregion // #region Public methods
    }
}
