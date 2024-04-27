using Azure.ServiceBus.API.Data.BusinessObject;
using Azure.ServiceBus.API.Data.BusinessObjects;
using Azure.ServiceBus.API.Data.SimpleModels;

namespace Azure.ServiceBus.API.Data.Core
{
    public class FeedProcessSBQueueCore : IFeedProcessSBQueueCore
    {
        private readonly IFeedProcessSBQueueBO _feedProcessSBQueueBO;
        private readonly IConfigurationSettings _configuration;
        public FeedProcessSBQueueCore(IConfigurationSettings configuration, IFeedProcessSBQueueBO feedProcessSBQueueBO)
        {
            _configuration = configuration;
            _feedProcessSBQueueBO = feedProcessSBQueueBO;
        }

        public Task<string> ReadMessage()
        {
           return _feedProcessSBQueueBO.ReadMessage();
        }

        public Task SendToQueueAsync(OrderSampleModel order)
        {
            return _feedProcessSBQueueBO.SendToQueueAsync(order);

        }
    }
}
