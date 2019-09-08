using AzureServiceFabric.TechTalk.Processor.Models;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AzureServiceFabric.TechTalk.Processor.Core
{
    /// <summary>
    /// Handles all of the Twilio related operations
    /// </summary>
    public class TwilioEngine : ITwilioEngine
    {
        /// <summary>
        /// Constructs with Twilio account SID and authentication token
        /// </summary>
        /// <param name="accountSid"></param>
        /// <param name="authToken"></param>
        public TwilioEngine(string accountSid, string authToken)
        {
            TwilioClient.Init(accountSid, authToken);
        }

        /// <summary>
        /// Sends out the message to the Twilio API
        /// </summary>
        /// <param name="message">Message to be sent out</param>
        /// <returns></returns>
        public async Task SendMessageAsync(Message message)
        {
            MessageResource messageResource = await MessageResource.CreateAsync(
            body: message.Body,
            from: new Twilio.Types.PhoneNumber(message.From),
            to: new Twilio.Types.PhoneNumber(message.To));
        }
    }
}
