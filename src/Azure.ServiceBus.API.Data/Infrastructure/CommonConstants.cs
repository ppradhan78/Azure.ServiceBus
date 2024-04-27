namespace Azure.ServiceBus.API.Data.Infrastructure
{
    public static class CommonConstants
    {

        public static string AzureStorageConnection = "DefaultEndpointsProtocol=https;AccountName=azpracticsstgact123;AccountKey=GfCqNBP5PZvb4hlTZkR7vU6wAHjmMVQxD7RN8E/WZqAAy5y2dx2oRoHSA4pw46vWV41TNVUC9/+7+AStWbimXA==;EndpointSuffix=core.windows.net";
        public static string blobContainerName = "input";
        public static string AzureStorageQueueName = "input";
        public static string AzureEventHubConnection = "Endpoint=sb://feedprocessserverlessapi-eventhub-ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=I1eJQSxgtAsN0iN479dQN1gzYUVlPc6H8+AEhKFjIPI=";
        public static string AzureEventHubNameSpace = "feedprocessserverlessapi-eventhub-ns.servicebus.windows.net";
        public static string AzureEventHubName = "feedprocessserverlessapi-eventhub";
        public static string AzureEventGridTopicEndpoint = "https://feedprocessserverlessapi-topic.westus-1.eventgrid.azure.net/api/events";
        public static string AzureEventGridTopicKey = "AQO6iaSQtBI0zU3ILgzn6guc/TfE2hOPOJpCKMmle3A=";
        public static string AzureEventGridTopicName = "feedprocessserverlessapi-topic";
        public static string AzureEventGridSubscription = "feedprocessserverlessapi-topic-sub";
      
        public static string CosmosDBAccountUri = "https://feedprocessserverlessapi.documents.azure.com:443/";
        public static string CosmosDBAccountPrimaryKey = "lGksXUOaVei0NLALtuBQm70f7gVi8Qg2k49Bn7izmogcUqWqUBhuA80rAOoRp3CUhVSP2IBE9tOCACDbhCL7VQ==";
        public static string CosmosDbName = "Northwind";
        public static string CosmosDbContainerName = "Order";



    }
}
