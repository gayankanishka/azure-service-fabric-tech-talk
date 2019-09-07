using AzureServiceFabric.TechTalk.Processor.Core;
using AzureServiceFabric.TechTalk.Processor.Models;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Processor
{
    /// <summary>
    /// Processor for the ingested messages
    /// </summary>
    public class IngestProcessor
    {
        #region Variables

        private const int MESSAGE_BATCH_COUNT = 10;
        private const string QUEUE_NAME = "messagesqueue";
        private const int MESSAGE_RETRY_COUNT = 3;

        private readonly ICloudStorage cloudStorage;
        private readonly ITwilioEngine twilioEngine;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the ingest processor
        /// </summary>
        /// <param name="cloudStorage">Injected cloud storage account</param>
        /// <param name="twilioEngine">Injected Twilio engine</param>
        public IngestProcessor(ICloudStorage cloudStorage, ITwilioEngine twilioEngine)
        {
            this.cloudStorage = cloudStorage;
            this.twilioEngine = twilioEngine;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves messages from the Storage queue and sends for processing
        /// </summary>
        /// <returns></returns>
        public async Task ProcessIngestMessages()
        {
            IEnumerable<CloudQueueMessage> queueMessages = await cloudStorage.GetQueueMessagesAsync(QUEUE_NAME, MESSAGE_BATCH_COUNT);

            IList<Task> taskList = new List<Task>();

            foreach (CloudQueueMessage message in queueMessages)
            {
                taskList.Add(ProceedToTwilio(message));
            }

            await Task.WhenAll(taskList);
        }

        // Process the queue message and sends to the Twilio
        private async Task ProceedToTwilio(CloudQueueMessage cloudQueueMessage)
        {
            if (cloudQueueMessage.DequeueCount > MESSAGE_RETRY_COUNT)
            {
                await cloudStorage.DeleteQueueMessageAsync(QUEUE_NAME, cloudQueueMessage);
                return;
            }

            Message message = await Task.Run(() => JsonConvert.DeserializeObject<Message>(cloudQueueMessage.AsString));

            await twilioEngine.SendMessageAsync(message);

            await cloudStorage.DeleteQueueMessageAsync(QUEUE_NAME, cloudQueueMessage);
        }

        #endregion
    }
}
