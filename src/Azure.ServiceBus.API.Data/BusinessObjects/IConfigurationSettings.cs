namespace Azure.ServiceBus.API.Data.BusinessObject
{
    public interface IConfigurationSettings
    {
        string AzureServiceBusTopicName { get; }
        string AzureServiceBusQueueName { get; }
        string AzureServiceBusNamespace { get; }
        string AzureServiceBusSubscriptionName { get; }
        string ConnectionString { get; }
    }
}
