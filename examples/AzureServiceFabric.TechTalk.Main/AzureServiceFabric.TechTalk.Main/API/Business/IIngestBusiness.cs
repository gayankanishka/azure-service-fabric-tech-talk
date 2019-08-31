using AzureServiceFabric.TechTalk.Main.Models;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Main.API.Business
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
