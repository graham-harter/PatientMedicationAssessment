using System;
using PatientMedication.Application.RequestDataClasses;

namespace PatientMedication.Application.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for updating a medication request.
    /// </summary>
    public interface IMedicationRequestUpdater
    {

        /// <summary>
        /// Update the specified medication request with the updated details contained within the <paramref name="request"/>.
        /// </summary>
        /// <param name="medicationRequestId">
        /// The id of the medication request to update.
        /// </param>
        /// <param name="request">
        /// The details of the new values to set on the specified medication request. This must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// <see cref="Task"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="request"/> argument is <c>null</c>.
        /// </exception>
        Task Update(
            int medicationRequestId,
            UpdateMedicationRequest request);
    }
}
