using Microsoft.Azure.Storage.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Processor.Core
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
        Task InsertQueueMessageAsync(string message);
        
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
        Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queueName, int messageCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudQueueMessage"></param>
        /// <returns></returns>
        Task DeleteQueueMessageAsync(CloudQueueMessage cloudQueueMessage);
    }
}
