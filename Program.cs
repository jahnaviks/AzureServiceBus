using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace ServiceBusImplementation
{
    class Program
    {
        //// connection string to your Service Bus namespace
        //static string connectionString = "Endpoint=sb://trainingtest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Ld18Eh/+zzlNJXeuFkRXBJXK1ity8btcdpdxfPlgnWE=";

        //// name of your Service Bus queue
        //static string queueName = "SampleQueue";

        //// the client that owns the connection and can be used to create senders and receivers
        //static ServiceBusClient client;

        //// the sender used to publish messages to the queue
        //static ServiceBusSender sender;

        //// number of messages to be sent to the queue
        ////private const int numOfMessages = 1;

        public static void Main()
        {
            Program program = new Program();
            program.GetMessageFromQueue();
            program.PutMessageInQueue();
        }

        public void PutMessageInQueue()
        {
            AzureBusSender sender = new AzureBusSender();
            while(true)
            {
                Console.WriteLine("1. To stop Please type END \n2. To Continue Please type message and press enter: ");
                var strMessage = Console.ReadLine();
                if (!(string.IsNullOrWhiteSpace(strMessage) || strMessage == "END"))
                {
                    sender.SendMessage(strMessage);
                    Console.WriteLine("Message sent successfully");
                }
                else if(strMessage == "END")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Empty Message. Please enter some text");
                }
            }
        }

        public void GetMessageFromQueue()
        {
            AzureBusReceiver receiver = new AzureBusReceiver();
            receiver.ReceiveMessage();
        }
    }
}