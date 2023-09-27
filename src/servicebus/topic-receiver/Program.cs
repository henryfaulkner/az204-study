using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

string _connectionString = "";
string _topicName = "salesperformancemessages";
string[] _subscriptions = new string[2];
_subscriptions[0] = "Americas";
_subscriptions[1] = "EuropeAndAsia";

ServiceBusClient client = new ServiceBusClient(_connectionString);
ServiceBusProcessorOptions options = new ServiceBusProcessorOptions
{
    MaxConcurrentCalls = 1,
    AutoCompleteMessages = false
};
List<ServiceBusProcessor> processors = new List<ServiceBusProcessor>();

foreach (string sub in _subscriptions)
{
    Console.WriteLine(sub);
    ServiceBusProcessor processor = client.CreateProcessor(_topicName, sub, options);
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;
    processors.Add(processor);

    await processor.StartProcessingAsync();
}

Thread.Sleep(2000);

foreach (ServiceBusProcessor processor in processors)
{
    await processor.StopProcessingAsync();
    await processor.DisposeAsync();
}

await client.DisposeAsync();

static async Task MessageHandler(ProcessMessageEventArgs args)
{
    Console.WriteLine($"Received message: SequenceNumber:{args.Message.SequenceNumber} Body:{args.Message.Body}");
    // complete the message so that message is deleted from the queue. 
    await args.CompleteMessageAsync(args.Message);
}


static Task ErrorHandler(ProcessErrorEventArgs args)
{
    // print the exception message
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}