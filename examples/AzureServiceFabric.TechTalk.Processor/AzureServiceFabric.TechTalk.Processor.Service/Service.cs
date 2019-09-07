using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
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
            // Gets the settings section
            ConfigurationSection azureConfigurationSection = FabricRuntime.GetActivationContext()?
                    .GetConfigurationPackageObject("Config")?
                    .Settings.Sections["AzureStorageConfigs"];

            ConfigurationSection twilioConfigurationSection = FabricRuntime.GetActivationContext()?
                    .GetConfigurationPackageObject("Config")?
                    .Settings.Sections["TwilioConfigs"];

            // Gets the settings from the config file
            string storageAccountKey = azureConfigurationSection?.Parameters["StorageConnectionString"]?.Value;
            string accountSid = twilioConfigurationSection?.Parameters["AccountSid"]?.Value;
            string authToken = twilioConfigurationSection?.Parameters["AuthToken"]?.Value;

            IServiceCollection serviceCollection = new ServiceCollection();

            ICloudStorage cloudStorage = new CloudStorage(storageAccountKey);

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
                catch (Exception)
                {
                    // Not throwing in order to run the service infinity
                }

                cancellationToken.ThrowIfCancellationRequested();

                // Could increase or decrease the queue processing interval here
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }
    }
}
