using System.Threading.Tasks;
using AzureServiceFabric.TechTalk.Main.Core;
using AzureServiceFabric.TechTalk.Main.Models;
using Newtonsoft.Json;

namespace AzureServiceFabric.TechTalk.Main.API.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class IngestBusiness : IIngestBusiness
    {
        #region Variables

        private readonly ICloudStorage cloudStorage;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudStorage"></param>
        public IngestBusiness(ICloudStorage cloudStorage)
        {
            this.cloudStorage = cloudStorage;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task IngestIntoStorageAsync(Message message)
        {
            await cloudStorage.InsertQueueMessageAsync(JsonConvert.SerializeObject(message));
        }

        #endregion
    }
}
