using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace ServiceBusImplementation
{
    public class AzureBusSender
    {
        // connection string to your Service Bus namespace
        static string connectionString = "Endpoint=sb://trainingtest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Ld18Eh/+zzlNJXeuFkRXBJXK1ity8btcdpdxfPlgnWE=";

        // name of your Service Bus queue
        static string queueName = "SampleQueue";

        // the client that owns the connection and can be used to create senders and receivers
        static ServiceBusClient client;

        // the sender used to publish messages to the queue
        static ServiceBusSender sender;
        public async void SendMessage(string message)
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // Create the clients that we'll use for sending and processing messages.
            client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(queueName);

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            //for (int i = 1; i <= numOfMessages; i++)
            //{
            //    // try adding a message to the batch
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
                {
                    // if it is too large for the batch
                    throw new Exception($"The message is too large to fit in the batch.");
                }
            //}

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"One message has been published to the queue.");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
