using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

string _connectionString = "";
string _topicName = "salesperformancemessages";

Console.WriteLine("Enter message:");
string messageContent = Console.ReadLine();

ServiceBusClient client = new ServiceBusClient(_connectionString);
ServiceBusSender sender = client.CreateSender(_topicName);
ServiceBusMessage message = new ServiceBusMessage(messageContent);
await sender.SendMessageAsync(message);