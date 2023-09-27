using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

string _connectionString = "";
string _queueName = "salesmessages";

ServiceBusClient client = new ServiceBusClient(_connectionString);
ServiceBusProcessorOptions options = new ServiceBusProcessorOptions
{
    MaxConcurrentCalls = 1,
    AutoCompleteMessages = false
};

ServiceBusProcessor processor = client.CreateProcessor(_queueName, options);
processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

await processor.StartProcessingAsync();
Console.Read();
await processor.CloseAsync();

static async Task MessageHandler(ProcessMessageEventArgs args)
{
    // extract the message
    string body = args.Message.Body.ToString();

    // print the message
    Console.WriteLine($"Received: {body}");

    // complete the message so that message is deleted from the queue. 
    await args.CompleteMessageAsync(args.Message);
}


static Task ErrorHandler(ProcessErrorEventArgs args)
{
    // print the exception message
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}