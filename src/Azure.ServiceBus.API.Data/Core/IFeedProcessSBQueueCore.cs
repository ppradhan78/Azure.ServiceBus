using Azure.ServiceBus.API.Data.SimpleModels;

namespace Azure.ServiceBus.API.Data.Core
{
    public interface IFeedProcessSBQueueCore
    {
        Task SendToQueueAsync(OrderSampleModel order);
        Task<string> ReadMessage();
    }
}
