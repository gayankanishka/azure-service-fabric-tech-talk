using AzureServiceFabric.TechTalk.Main.Models;
using System.Threading.Tasks;

namespace AzureServiceFabric.TechTalk.Main.API.Business
{
    public interface IIngestBusiness
    {
        //TODO: Comments
        Task IngestIntoStorageAsync(Message message);
    }
}
