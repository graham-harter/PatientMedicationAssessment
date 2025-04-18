﻿using System;
using PatientMedication.Infrastructure.Configuration.Interfaces;
using PatientMedication.Infrastructure.DbContexts;
using PatientMedication.Infrastructure.Factories.Interfaces;

namespace PatientMedication.Infrastructure.Factories
{
    /// <summary>
    /// Class responsible for creating instances of <see cref="PatientMedicationContextFactory"/>.
    /// </summary>
    internal sealed class PatientMedicationContextFactory : IPatientMedicationContextFactory
    {
        private readonly IPatientMedicationContextConfigurer _configurer;

        #region Constructor(s)

        public PatientMedicationContextFactory(
            IPatientMedicationContextConfigurer configurer)
        {
            // Validate argument(s).
            if (configurer is null) throw new ArgumentNullException(nameof(configurer));

            // Make these arguments available to the object.
            _configurer = configurer;
        }

        #endregion // #region Constructor(s)

        #region Public methods

        public PatientMedicationContext Create()
        {
            return new PatientMedicationContext(_configurer);
        }

        #endregion // #region Public methods
    }
}
