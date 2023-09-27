using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

string _connectionString = "";
string _queueName = "salesmessages";

Console.WriteLine("Enter message:");
string messageContent = Console.ReadLine();

ServiceBusClient client = new ServiceBusClient(_connectionString);
ServiceBusSender sender = client.CreateSender(_queueName);
ServiceBusMessage message = new ServiceBusMessage(messageContent);
await sender.SendMessageAsync(message);