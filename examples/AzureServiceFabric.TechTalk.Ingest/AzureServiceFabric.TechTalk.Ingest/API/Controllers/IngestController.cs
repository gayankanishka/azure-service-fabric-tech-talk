using AzureServiceFabric.TechTalk.Ingest.API.Business;
using AzureServiceFabric.TechTalk.Ingest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Ingest.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("service-fabric/[controller]")]
    public class IngestController : ControllerBase
    {
        #region Variables

        private readonly IIngestBusiness ingestBusiness;

        #endregion

        #region Constructor

        public IngestController(IIngestBusiness ingestBusiness)
        {
            this.ingestBusiness = ingestBusiness;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostMessageAsync([FromBody] Message message)
        {
            try
            {
                message.TransactionId = Guid.NewGuid();

                await ingestBusiness.IngestIntoStorageAsync(message);

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
