using System;
using PatientMedication.Application.RequestDataClasses;

namespace PatientMedication.Application.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for creating a medication request.
    /// </summary>
    public interface IMedicationRequestCreator
    {
        Task<int> Create(
            CreateMedicationRequest request);
    }
}
