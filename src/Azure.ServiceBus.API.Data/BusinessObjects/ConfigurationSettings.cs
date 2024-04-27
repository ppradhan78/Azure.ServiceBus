using Microsoft.Extensions.Configuration;

namespace Azure.ServiceBus.API.Data.BusinessObject
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        #region Global Variable(s)
        private readonly IConfiguration _configuration;
        #endregion

        public ConfigurationSettings(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }
        #region Public Prop(s)

        public string AzureServiceBusTopicName => _configuration["ServiceBus:TopicName"];

        public string AzureServiceBusQueueName => _configuration["ServiceBus:QueueName"];

        public string AzureServiceBusNamespace => _configuration["ServiceBus:Namespace"];

        public string AzureServiceBusSubscriptionName => _configuration["ServiceBus:SubscriptionName"];

        public string ConnectionString => _configuration["ServiceBus:ConnectionString"];

        #endregion

    }
}
