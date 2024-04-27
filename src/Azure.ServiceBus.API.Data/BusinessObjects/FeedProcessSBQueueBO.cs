using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Azure.ServiceBus.API.Data.BusinessObject;
using Azure.ServiceBus.API.Data.SimpleModels;

namespace Azure.ServiceBus.API.Data.BusinessObjects
{
    public class FeedProcessSBQueueBO : IFeedProcessSBQueueBO
    {
        string Message = "";

        private readonly IConfigurationSettings _configuration;
        public FeedProcessSBQueueBO(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }
        async Task MessageHandler(ProcessMessageEventArgs args)
        {
             Message = args.Message.Body.ToString();
            Console.WriteLine($"Received: {Message}");

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
        public async Task<string> ReadMessage()
        {
            var client = GetServiceBusClient();
            ServiceBusProcessor  processor = client.CreateProcessor(_configuration.AzureServiceBusQueueName, new ServiceBusProcessorOptions());
            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                
                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;
                
                // start processing 
                await processor.StartProcessingAsync();

                // stop processing 
                await processor.StopProcessingAsync();
            }
            catch (Exception ex) { }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
            return Message;
        }



        public async Task SendToQueueAsync(OrderSampleModel order)
        {
            var client = GetServiceBusClient();
            ServiceBusSender sender = client.CreateSender(_configuration.AzureServiceBusQueueName);
            try
            {
                int numOfMessages = 2;
                using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
                for (int i = 1; i <= numOfMessages; i++)
                {
                    // try adding a message to the batch
                    if (!messageBatch.TryAddMessage(new ServiceBusMessage(Newtonsoft.Json.JsonConvert.SerializeObject(order))))
                    {
                        // if it is too large for the batch
                        throw new Exception($"The message {i} is too large to fit in the batch.");
                    }
                }
                await sender.SendMessagesAsync(messageBatch);
            }
            catch (Exception ex) { }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

        }

        private ServiceBusClient GetServiceBusClient()
        {
            try
            {
                var clientOptions = new ServiceBusClientOptions
                {
                    TransportType = ServiceBusTransportType.AmqpWebSockets
                };
                //return new ServiceBusClient(_configuration.AzureServiceBusNamespace+ ".servicebus.windows.net",
                //     new DefaultAzureCredential(), clientOptions);
                return new ServiceBusClient(_configuration.ConnectionString);
                //return new ServiceBusClient(_configuration.ConnectionString, new DefaultAzureCredential());
                // return new ServiceBusClient(_configuration.ConnectionString, clientOptions);
                //return new ServiceBusClient(_configuration.ConnectionString, clientOptions);


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
