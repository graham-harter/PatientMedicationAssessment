using System;
using PatientMedication.Application.Interfaces;
using PatientMedication.Application.RequestDataClasses;
using PatientMedication.Infrastructure.Factories.Interfaces;
using PatientMedication.Infrastructure.Models;

namespace PatientMedication.Application
{
    /// <summary>
    /// Class responsible for updating a medication request.
    /// </summary>
    internal sealed class MedicationRequestUpdater : IMedicationRequestUpdater
    {
        private readonly IPatientMedicationContextFactory _patientMedicationContextFactory;

        public MedicationRequestUpdater(
            IPatientMedicationContextFactory patientMedicationContextFactory)
        {
            // Validate argument(s).
            if (patientMedicationContextFactory is null) throw new ArgumentNullException(nameof(patientMedicationContextFactory));

            // Make these arguments available to the object.
            _patientMedicationContextFactory = patientMedicationContextFactory;
        }

        public async Task Update(
            int medicationRequestId,
            UpdateMedicationRequest request)
        {
            // Validate argument(s).
            if (request is null) throw new ArgumentNullException(nameof(request));

            using var context = _patientMedicationContextFactory.Create();

            var frequencyUnit = request.FrequencyUnit?.Trim();
            var medicationRequestStatus = request.Status?.Trim();

            MedicationRequestStatus? requestStatus = null!;
            short? requestStatusId = null!;
            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                // todo: Provide meaningful error message if this data cannot be found.
                requestStatus = context.MedicationRequestStatuses.First(rs => rs.Description == medicationRequestStatus);
                requestStatusId = requestStatus.MedicationRequestStatusId;
            }

            // todo: Lookup the FrequencyUnitId for the specified FrequencyUnit description.

            // Get the specified medication request.
            var medicationRequest = await context.FindAsync<MedicationRequest>(medicationRequestId);
            if (medicationRequest is null)
            {
                throw new KeyNotFoundException(
                    $"The medication request with Id {medicationRequestId} cannot be found");
            }

            // Update the properties of this medication request.
            if (request.EndDate.HasValue) medicationRequest!.EndDate = request.EndDate.Value;
            if (request.FrequencyNumberOfTimes.HasValue)
            {
                medicationRequest!.FrequencyNumberOfTimes = request.FrequencyNumberOfTimes.Value;
                medicationRequest!.FrequencyUnitId = 0;     // todo: Use an entity to get an id value for this property.
            }
            if (requestStatusId.HasValue)
            {
                medicationRequest!.MedicationRequestStatusId = requestStatusId.Value;
            }

            await context.SaveChangesAsync();
        }
    }
}
