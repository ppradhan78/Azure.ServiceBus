using Azure.ServiceBus.API.Data.SimpleModels;

namespace Azure.ServiceBus.API.Data.BusinessObjects
{
    public interface IFeedProcessSBQueueBO
    {
        Task SendToQueueAsync(OrderSampleModel order);
        Task<string> ReadMessage();
    }
}
