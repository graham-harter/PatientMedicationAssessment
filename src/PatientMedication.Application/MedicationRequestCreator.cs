using System;
using PatientMedication.Application.Interfaces;
using PatientMedication.Application.RequestDataClasses;
using PatientMedication.Infrastructure.Factories.Interfaces;
using PatientMedication.Infrastructure.Models;

namespace PatientMedication.Application
{
    /// <summary>
    /// Class responsible for creating a medication request.
    /// </summary>
    internal sealed class MedicationRequestCreator : IMedicationRequestCreator
    {
        private readonly IPatientMedicationContextFactory _patientMedicationContextFactory;

        public MedicationRequestCreator(
            IPatientMedicationContextFactory patientMedicationContextFactory)
        {
            // Validate argument(s).
            if (patientMedicationContextFactory is null) throw new ArgumentNullException(nameof(patientMedicationContextFactory));

            // Make these arguments available to the object.
            _patientMedicationContextFactory = patientMedicationContextFactory;
        }

        public async Task<int> Create(
            CreateMedicationRequest request)
        {
            // Validate argument(s).
            if (request is null) throw new ArgumentNullException(nameof(request));

            using var context = _patientMedicationContextFactory.Create();

            var medicationReference = request.MedicationReference!.Trim();
            var medicationRequestStatus = request.Status!.Trim();

            // todo: Provide meaningful error messages if any of this data cannot be found.
            var patient = context.Patients.First(p => p.PatientReference == request.PatientReference);
            var clinican = context.Clinicians.First(c => c.ClinicianReference == request.ClinicianReference);
            var medication = context.Medications.First(m => m.Code == medicationReference);
            var requestStatus = context.MedicationRequestStatuses.First(rs => rs.Description == medicationRequestStatus);

            var medicationRequest = new MedicationRequest
            {
                PatientReference = request.PatientReference!.Value,
                ClinicianReference = request.ClinicianReference!.Value,
                MedicationReference = medicationReference,
                ReasonText = request.ReasonText!.Trim(),
                PrescribedDate = request.PrescribedDate!.Value,
                StartDate = request.StartDate!.Value,
                EndDate = request.EndDate,
                FrequencyNumberOfTimes = request.FrequencyNumberOfTimes!.Value,
                FrequencyUnitId = 0,        // todo: Use an entity to get an id value for this property.
                MedicationRequestStatusId = requestStatus.MedicationRequestStatusId,
            };

            context.MedicationRequests.Add(medicationRequest);

            await context.SaveChangesAsync();

            var medicationRequestId = medicationRequest.MedicationRequestId;
            return medicationRequestId;
        }
    }
}
