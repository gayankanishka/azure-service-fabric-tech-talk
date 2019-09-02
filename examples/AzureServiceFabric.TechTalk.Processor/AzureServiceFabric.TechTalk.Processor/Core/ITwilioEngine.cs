using AzureServiceFabric.TechTalk.Processor.Models;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Processor.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITwilioEngine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudQueueMessage"></param>
        /// <returns></returns>
        Task SendMessageAsync(Message message);
    }
}
