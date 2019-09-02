using AzureServiceFabric.TechTalk.Processor.Models;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AzureServiceFabric.TechTalk.Processor.Core
{
    public class TwilioEngine : ITwilioEngine
    {
        public TwilioEngine(string accountSid, string authToken)
        {
            TwilioClient.Init(accountSid, authToken);
        }

        public async Task SendMessageAsync(Message message)
        {
            MessageResource messageResource = await MessageResource.CreateAsync(
            body: message.Body,
            from: new Twilio.Types.PhoneNumber(message.From),
            to: new Twilio.Types.PhoneNumber(message.To));
        }
    }
}
