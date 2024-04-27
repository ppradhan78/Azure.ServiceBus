using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Azure.ServiceBus.API.Data.BusinessObject;
using Azure.ServiceBus.API.Data.SimpleModels;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Azure.ServiceBus.API.Data.BusinessObjects
{
    public class FeedProcessSBTopicBO : IFeedProcessSBTopicBO
    {

        private readonly IConfigurationSettings _configuration;
        public FeedProcessSBTopicBO(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ReadMessage()
        {
            string Message = "";
            var client = GetServiceBusClient();
            ServiceBusProcessor processor = client.CreateProcessor(_configuration.AzureServiceBusQueueName, new ServiceBusProcessorOptions());

            try
            {
              
                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;
                async Task MessageHandler(ProcessMessageEventArgs args)
                {
                    string body = args.Message.Body.ToString();
                    Console.WriteLine($"Received: {body}");

                    // complete the message. message is deleted from the queue. 
                    await args.CompleteMessageAsync(args.Message);
                }

                // handle any errors when receiving messages
                Task ErrorHandler(ProcessErrorEventArgs args)
                {
                    Console.WriteLine(args.Exception.ToString());
                    return Task.CompletedTask;
                }
                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
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

            int numOfMessages = 1;
            var client = GetServiceBusClient();
            ServiceBusSender sender = client.CreateSender(_configuration.AzureServiceBusTopicName);
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
            try
            {

                // Use the producer client to send the batch of messages to the Service Bus queue
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
                return new ServiceBusClient(_configuration.ConnectionString);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
