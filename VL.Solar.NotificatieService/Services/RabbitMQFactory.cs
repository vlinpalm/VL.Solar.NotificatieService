using RabbitMQ.Client;

namespace VL.Solar.NotificatieService.Services;

public static class RabbitMQFactory
{
    public static ConnectionFactory CreateConnectionFactory()
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

        return connectionFactory;
    }
}