using Microsoft.Azure.Storage.Queue;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Main.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICloudStorage
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        Task CreateQueueIfNotFoundAsync(string queueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<CloudQueueMessage> GetQueueMessageAsync(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task InsertQueueMessageAsync(string message);
    }
}
