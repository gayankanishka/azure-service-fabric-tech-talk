using AzureServiceFabric.TechTalk.Processor.Models;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Processor.Core
{
    /// <summary>
    /// Handles all of the Twilio related operations
    /// </summary>
    public interface ITwilioEngine
    {
        /// <summary>
        /// Sends out the message to the Twilio API
        /// </summary>
        /// <param name="message">Message to be sent out</param>
        /// <returns></returns>
        Task SendMessageAsync(Message message);
    }
}
