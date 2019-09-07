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
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task InsertQueueMessageAsync(string queueName, string message)
        {
            CloudQueue queue = cloudQueueClient.GetQueueReference(queueName);
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(message);

            await queue.CreateIfNotExistsAsync();

            await queue.AddMessageAsync(cloudQueueMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task<CloudQueueMessage> GetQueueMessageAsync(string queueName)
        {
            CloudQueue queue = cloudQueueClient.GetQueueReference(queueName);

            return await queue.GetMessageAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queueName, int messageCount)
        {
            CloudQueue queue = cloudQueueClient.GetQueueReference(queueName);

            return await queue.GetMessagesAsync(messageCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// /// <param name="cloudQueueMessage"></param>
        /// <returns></returns>
        public async Task DeleteQueueMessageAsync(string queueName, CloudQueueMessage cloudQueueMessage)
        {
            CloudQueue queue = cloudQueueClient.GetQueueReference(queueName);

            await queue.DeleteMessageAsync(cloudQueueMessage);
        }
    }
}
