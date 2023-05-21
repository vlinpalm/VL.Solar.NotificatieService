using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace VL.Solar.SolarWorker.Services;

public class NotificatieConsumerService
{
    private readonly ConnectionFactory connectionFactory;
    private readonly HubConnection hubConnection;

    public NotificatieConsumerService(ConnectionFactory connectionFactory, HubConnection hubConnection)
    {
        this.connectionFactory = connectionFactory;
        this.hubConnection = hubConnection;
    }

    public async Task ConsumeMessages(string queueName)
    {
        await hubConnection.StartAsync();

        using (var connection = connectionFactory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queueName, true, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, args) =>
            {
                var messageBytes = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(messageBytes);
                await hubConnection.SendAsync("StuurtBerichtNaarGroep", message);
            };

            channel.BasicConsume(queueName, true, consumer);

            Console.WriteLine("Wacht op berichten...");
            Console.ReadLine();
        }
    }

}