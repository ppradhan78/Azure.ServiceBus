using Azure.ServiceBus.API.Data.BusinessObject;
using Azure.ServiceBus.API.Data.BusinessObjects;
using Azure.ServiceBus.API.Data.SimpleModels;

namespace Azure.ServiceBus.API.Data.Core
{
    public class FeedProcessSBTopicCore : IFeedProcessSBTopicCore
    {
        private readonly IFeedProcessSBTopicBO _feedProcessSBTopicBO;
        private readonly IConfigurationSettings _configuration;
        public FeedProcessSBTopicCore(IConfigurationSettings configuration, IFeedProcessSBTopicBO feedProcessSBTopicBO)
        {
            _configuration = configuration;
            _feedProcessSBTopicBO = feedProcessSBTopicBO;
        }

        public Task<string> ReadMessage()
        {
           return _feedProcessSBTopicBO.ReadMessage();
        }

        public Task SendToQueueAsync(OrderSampleModel order)
        {
            return _feedProcessSBTopicBO.SendToQueueAsync(order);

        }
    }
}
