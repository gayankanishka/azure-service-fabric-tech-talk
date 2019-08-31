using AzureServiceFabric.TechTalk.Ingest.Models;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Ingest.API.Business
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIngestBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task IngestIntoStorageAsync(Message message);
    }
}
