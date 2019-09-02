using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;

namespace AzureServiceFabric.TechTalk.Processor.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CloudStorage : ICloudStorage
    {
        private readonly CloudStorageAccount cloudStorageAccount;
        private CloudQueueClient cloudQueueClient;
        private CloudQueue queue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CloudStorage(string connectionString)
        {
            cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task CreateQueueIfNotFoundAsync(string queueName)
        {
            queue = cloudQueueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task InsertQueueMessageAsync(string message)
        {
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(message);
            await queue.AddMessageAsync(cloudQueueMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<CloudQueueMessage> GetQueueMessageAsync(string message)
        {
            CloudQueueMessage cloudQueueMessage = await queue.GetMessageAsync();
            await queue.DeleteMessageAsync(cloudQueueMessage);
            return cloudQueueMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queueName, int messageCount)
        {
            return await queue.GetMessagesAsync(messageCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task DeleteQueueMessageAsync(CloudQueueMessage cloudQueueMessage)
        {
            await queue.DeleteMessageAsync(cloudQueueMessage);
        } 
    }
}
