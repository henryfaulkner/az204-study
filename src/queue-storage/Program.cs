using Azure;
using Azure.Identity;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Threading.Tasks;

Console.WriteLine("Enter Client Name: ");
string clientName = Console.ReadLine();
string queueName = $"{clientName}-{Guid.NewGuid().ToString()}";
string storageAccountName = "az204sheesh";
string connectionString = "DefaultEndpointsProtocol=https;AccountName=az204sheesh;AccountKey=m08OVbAbkl2H6mtqnDqdESKTzlQVCuza1RbGJ3m6PwrC32dcPqkueB6EsHl4+irLZmbYGBY3Vawh+AStunNOLw==;EndpointSuffix=core.windows.net";

Console.WriteLine("Creating QueueClient.");
QueueClient queueClient = new QueueClient(connectionString, queueName);

Console.WriteLine($"Creating queue: {queueName}");
await queueClient.CreateAsync();

try {
    Console.WriteLine("\nAdding messages to the queue...");
    await queueClient.SendMessageAsync("First message");
    await queueClient.SendMessageAsync("Second message");
    SendReceipt receipt = await queueClient.SendMessageAsync("Third message");
}
catch (RequestFailedException ex)
{
    Console.WriteLine($"Azure.RequestFailedException: {ex.Message}");
    Console.WriteLine($"Status code: {ex.Status}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}