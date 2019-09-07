using System.Threading.Tasks;
using AzureServiceFabric.TechTalk.Ingest.Core;
using AzureServiceFabric.TechTalk.Ingest.Models;
using Newtonsoft.Json;

namespace AzureServiceFabric.TechTalk.Ingest.API.Business
{
    /// <summary>
    /// Handles the business logic of the Ingest API
    /// </summary>
    public class IngestBusiness : IIngestBusiness
    {
        #region Variables

        private const string QUEUE_NAME = "messagesqueue";
        private readonly ICloudStorage _cloudStorage;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="cloudStorage">Injected cloud storage account</param>
        public IngestBusiness(ICloudStorage cloudStorage)
        {
            _cloudStorage = cloudStorage;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts the message into the queue
        /// </summary>
        /// <param name="message">Message to be inserted</param>
        /// <returns></returns>
        public async Task IngestIntoStorageAsync(Message message)
        {
            await _cloudStorage.InsertQueueMessageAsync(QUEUE_NAME, JsonConvert.SerializeObject(message));
        }

        #endregion
    }
}
