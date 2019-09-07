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
        /// <param name="message"></param>
        /// <returns></returns>
        Task InsertQueueMessageAsync(string queueName, string message);

        /// <summary>
        ///
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        Task<CloudQueueMessage> GetQueueMessageAsync(string queueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageCount"></param>
        /// <returns></returns>
        Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queueName, int messageCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="cloudQueueMessage"></param>
        /// <returns></returns>
        Task DeleteQueueMessageAsync(string queueName, CloudQueueMessage cloudQueueMessage);
    }
}
