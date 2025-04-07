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
        private readonly IMedicationRequestRetrieverByFilterCriteria _medicationRequestRetrieverByFilterCriteria;
        private readonly ILogger<PatientMedicationController> _logger;

        #region Constructor(s)

        public PatientMedicationController(
            IMedicationRequestCreator medicationRequestCreator,
            IMedicationRequestRetrieverByFilterCriteria medicationRequestRetrieverByFilterCriteria,
            ILogger<PatientMedicationController> logger)
        {
            // Validate argument(s).
            if (medicationRequestCreator is null) throw new ArgumentNullException(nameof(medicationRequestCreator));
            if (medicationRequestRetrieverByFilterCriteria is null) throw new ArgumentNullException(nameof(medicationRequestRetrieverByFilterCriteria));
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            // Make these arguments available to the object.
            _medicationRequestCreator = medicationRequestCreator;
            _medicationRequestRetrieverByFilterCriteria = medicationRequestRetrieverByFilterCriteria;
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

        [HttpGet]
        public async Task<IActionResult> GetMedicationRequests(
            [FromQuery] string? status,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? finishDate)
        {
            // Validate arguments.
            if (string.IsNullOrWhiteSpace(status)
                && !startDate.HasValue
                && !finishDate.HasValue)
            {
                throw new ArgumentNullException(
                    nameof(status),
                    "You must specify at least one filter criterion.");
            }

            if (startDate.HasValue != finishDate.HasValue)
            {
                throw new ArgumentException(
                    $"Either the {nameof(startDate)} and {nameof(finishDate)} arguments should be specified, or neither should be specified",
                    nameof(finishDate));
            }

            try
            {
                _logger
                    .LogDebug(
                        "Received request to get medication requests. Arguments: Status = {Status}; StartDate = {StartDate}; FinishDate = {FinishDate}.",
                        status,
                        startDate,
                        finishDate);

                var medicationRequests = await _medicationRequestRetrieverByFilterCriteria.GetMedicationRequests(
                    status,
                    startDate,
                    finishDate);

                _logger
                    .LogDebug("Successfully retrieved {NumberOfRecords} medication requests for the supplied criteria. Arguments: Status = {Status}; StartDate = {StartDate}; FinishDate = {FinishDate}.",
                    medicationRequests.Count,
                    status,
                    startDate,
                    finishDate);

                return new OkObjectResult(medicationRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred when attempting to retrieve medication requests for the supplied criteria. Arguments: Status = {Status}; StartDate = {StartDate}; FinishDate = {FinishDate}.",
                    status,
                    startDate,
                    finishDate);

                throw;
            }

        }

        #endregion // #region Public methods
    }
}
