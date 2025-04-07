using System;
using PatientMedication.Application.ReturnDataClasses;
using PatientMedication.Application.ReturnDataClasses;

namespace PatientMedication.Application.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for retrieving medication requests by filter criteria.
    /// </summary>
    public interface IMedicationRequestRetrieverByFilterCriteria
    {
        /// <summary>
        /// Get medication requests for the supplied filter criteria.
        /// </summary>
        /// <param name="status">
        /// (Optional) The description of the medication request status by which to filter medication requests.
        /// </param>
        /// <param name="startDate">
        /// (Optional) The medication prescription start date by which to filter medication requests.
        /// <para>
        /// </param>
        /// <param name="finishDate">
        /// (Optional) The medication prescription finish date by which to filter medication requests.
        /// </param>
        /// <returns>
        /// List of medication requests matching the supplied filter criteria.
        /// </returns>
        Task<List<MedicationRequest>> GetMedicationRequests(
            string? status,
            DateTime? startDate,
            DateTime? finishDate);
    }
}
