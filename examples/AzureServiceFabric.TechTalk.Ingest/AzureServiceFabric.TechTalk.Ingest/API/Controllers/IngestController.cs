using AzureServiceFabric.TechTalk.Ingest.API.Business;
using AzureServiceFabric.TechTalk.Ingest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Ingest.API.Controllers
{
    /// <summary>
    /// Ingest controller of the API
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("service-fabric/[controller]")]
    public class IngestController : ControllerBase
    {
        #region Variables

        private readonly IIngestBusiness _ingestBusiness;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the Ingest controller
        /// </summary>
        /// <param name="ingestBusiness">Injected business object</param>
        public IngestController(IIngestBusiness ingestBusiness)
        {
            _ingestBusiness = ingestBusiness;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Posts a message contract
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /service-fabric/Ingest
        ///     {
        ///        "from": "+0000000000",
        ///        "to": "+0000000000",
        ///        "body": "Add message body"
        ///     }
        ///
        /// </remarks>
        /// <param name="message">Message contract</param>
        /// <returns>A GUID to track the message</returns>
        /// <response code="202">Returns GUID for tracking purpose</response>
        /// <response code="400">If invalid payload is passed</response>
        /// <response code="500">If something went wrong in the server end</response>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostMessageAsync([FromBody] Message message)
        {
            try
            {
                message.TransactionId = Guid.NewGuid();

                await _ingestBusiness.IngestIntoStorageAsync(message);

                return Accepted(message.TransactionId);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        #endregion
    }
}
