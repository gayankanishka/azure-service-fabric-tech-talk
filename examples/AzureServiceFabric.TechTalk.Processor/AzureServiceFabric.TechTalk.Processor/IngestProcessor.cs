using AzureServiceFabric.TechTalk.Processor.Core;
using AzureServiceFabric.TechTalk.Processor.Models;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Processor
{
    public class IngestProcessor
    {
        #region Variables

        private const int MESSAGE_BATCH_COUNT = 10;
        private const string QUEUE_NAME = "messagequeue";
        private const int MESSAGE_RETRY_COUNT = 3;

        private readonly ICloudStorage cloudStorage;
        private readonly ITwilioEngine twilioEngine;

        #endregion

        #region Constructor

        public IngestProcessor(ICloudStorage cloudStorage, ITwilioEngine twilioEngine)
        {
            this.cloudStorage = cloudStorage;
            this.twilioEngine = twilioEngine;
        }

        #endregion

        #region Methods

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

        private async Task ProceedToTwilio(CloudQueueMessage cloudQueueMessage)
        {
            if (cloudQueueMessage.DequeueCount > MESSAGE_RETRY_COUNT)
            {
                await cloudStorage.DeleteQueueMessageAsync(cloudQueueMessage);
            }

            Message message = JsonConvert.DeserializeObject<Message>(cloudQueueMessage.AsString);

            await twilioEngine.SendMessageAsync(message);

            await cloudStorage.DeleteQueueMessageAsync(cloudQueueMessage);
        }

        #endregion
    }
}
