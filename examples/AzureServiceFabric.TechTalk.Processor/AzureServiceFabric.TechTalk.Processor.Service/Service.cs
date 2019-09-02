using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzureServiceFabric.TechTalk.Processor.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace AzureServiceFabric.TechTalk.Processor.Service
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Service : StatelessService
    {
        private const double QUEUE_PROCESSING_INTERVAL = 10;
        private ServiceProvider serviceProvider;
        private IngestProcessor ingestProcessor;

        public Service(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            // TODO: Need to get these configs from table storage
            string storageAccountKey = "UseDevelopmentStorage=true;";
            string queuename = "messagequeue";
            // Add your twilio account ID's
            string accountSid = "AC8bcda9120492208f3242accc1ebdd290";
            string authToken = "103f5b05252c252397db7138636f263b";
           
            IServiceCollection serviceCollection = new ServiceCollection();

            ICloudStorage cloudStorage = new CloudStorage(storageAccountKey);
            cloudStorage.CreateQueueIfNotFoundAsync(queuename);

            ITwilioEngine twilioEngine = new TwilioEngine(accountSid, authToken);

            serviceCollection.AddSingleton(cloudStorage)
                .AddSingleton(twilioEngine)
                .AddTransient<IngestProcessor, IngestProcessor>();

            serviceProvider = serviceCollection.BuildServiceProvider();

            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            ingestProcessor = serviceProvider.GetService<IngestProcessor>();

            while (true)
            {
                try
                {
                    await ingestProcessor.ProcessIngestMessages();
                }
                catch (Exception exception)
                {
                    // Not throwing in order to run the service infinity
                }

                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(TimeSpan.FromSeconds(QUEUE_PROCESSING_INTERVAL), cancellationToken);
            }
        }
    }
}
