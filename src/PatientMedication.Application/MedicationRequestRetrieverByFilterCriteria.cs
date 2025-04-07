using System;
using Microsoft.EntityFrameworkCore;
using PatientMedication.Application.Interfaces;
using PatientMedication.Infrastructure.Factories.Interfaces;
using PatientMedication.Infrastructure.Models;
using rdt = PatientMedication.Application.ReturnDataClasses;

namespace PatientMedication.Application
{
    /// <summary>
    /// Class responsible for retrieving medication requests by filter criteria.
    /// </summary>
    internal sealed class MedicationRequestRetrieverByFilterCriteria : IMedicationRequestRetrieverByFilterCriteria
    {
        private readonly IPatientMedicationContextFactory _patientMedicationContextFactory;

        #region Constructor(s)

        public MedicationRequestRetrieverByFilterCriteria(
            IPatientMedicationContextFactory patientMedicationContextFactory)
        {
            // Validate argument(s).
            if (patientMedicationContextFactory is null) throw new ArgumentNullException(nameof(patientMedicationContextFactory));

            // Make these arguments available to the object.
            _patientMedicationContextFactory = patientMedicationContextFactory;
        }

        #endregion // #region Constructor(s)

        #region Public methods

        public async Task<List<rdt.MedicationRequest>> GetMedicationRequests(
            string? status,
            DateTime? startDate,
            DateTime? finishDate)
        {
            using var context = _patientMedicationContextFactory.Create();

            // Ensure the supplied arguments are in a canonical form.
            status = status?.Trim();
            startDate = startDate.HasValue
                ? SetTime(startDate.Value, 00, 00, 00)
                : null!;
            finishDate = finishDate.HasValue
                ? SetTime(finishDate.Value, 23, 59, 59)
                : null!;

            // Get the medication status id corresponding to this value;
            int? requestStatusId = null!;
            MedicationRequestStatus? requestStatus = null!;
            if (!string.IsNullOrWhiteSpace(status))
            {
                // todo: Provide meaningful error message if the status id cannot be found.
                requestStatus = context.MedicationRequestStatuses.First(rs => rs.Description == status);
                requestStatusId = requestStatus.MedicationRequestStatusId;
            }

            // Retrieve medication requests by these filter criteria.
            // todo: Re-work this to work server-side so that we aren't returning all medication requests to the client.
            var medicationRequests = context.MedicationRequests
                .AsNoTracking()
                .Include(mr => mr.Clinician)
                .Include(mr => mr.Medication)
                .AsEnumerable()
                .Where(mr =>
                    (!requestStatusId.HasValue || mr.MedicationRequestStatusId == requestStatusId.Value)
                    &&
                    (!startDate.HasValue || mr.PrescribedDate >= startDate.Value)
                    &&
                    (!finishDate.HasValue || mr.PrescribedDate <= finishDate.Value))
                .Select(mr => new rdt.MedicationRequest(
                    mr.MedicationRequestId,
                    mr.Medication!.CodeName,
                    mr.Clinician!.FirstName,
                    mr.Clinician!.Surname))
                .OrderBy(mr => mr.MedicationRequestId)
                .ToList();

            return await Task.FromResult(medicationRequests);
        }

        #endregion // #region Public methods

        #region Private methods

        private static DateTime SetTime(DateTime dateTime, int hour, int minute, int second)
        {
            var result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, second);
            return result;
        }

        #endregion // #region Private methods
    }
}
