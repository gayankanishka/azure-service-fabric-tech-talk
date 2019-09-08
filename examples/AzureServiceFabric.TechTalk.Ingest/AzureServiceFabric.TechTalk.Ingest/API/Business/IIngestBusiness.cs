using AzureServiceFabric.TechTalk.Ingest.Models;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Ingest.API.Business
{
    /// <summary>
    /// Handles the business logic of the Ingest API
    /// </summary>
    public interface IIngestBusiness
    {
        /// <summary>
        /// Inserts the message into the queue
        /// </summary>
        /// <param name="message">Message to be inserted</param>
        /// <returns></returns>
        Task IngestIntoStorageAsync(Message message);
    }
}
