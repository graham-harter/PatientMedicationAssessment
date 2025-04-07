using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientMedication.Application.Interfaces;
using PatientMedication.Application.RequestDataClasses;

namespace PatientMedicationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class PatientMedicationController
    {
        private readonly IMedicationRequestCreator _medicationRequestCreator;
        private readonly ILogger<PatientMedicationController> _logger;

        #region Constructor(s)

        public PatientMedicationController(
            IMedicationRequestCreator medicationRequestCreator,
            ILogger<PatientMedicationController> logger)
        {
            // Validate argument(s).
            if (medicationRequestCreator is null) throw new ArgumentNullException(nameof(medicationRequestCreator));
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            // Make these arguments available to the object.
            _medicationRequestCreator = medicationRequestCreator;
            _logger = logger;
        }

        #endregion // #region Constructor(s)

        #region Public methods

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMedicationRequest(
            [FromBody, Required] CreateMedicationRequest request)
        {
            // Validate arguments.
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger
                    .LogDebug("Received request to create medication request. Request is: {Request}", request);

                var medicationRequestId = await _medicationRequestCreator.Create(request);

                _logger
                    .LogInformation("Successfully created medication request with Id {Id} from request {Request}",
                    medicationRequestId,
                    request);

                return new OkObjectResult(medicationRequestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred when attempting to create the medication request. Request was: {Request}", request);

                throw;
            }
        }

        #endregion // #region Public methods
    }
}
