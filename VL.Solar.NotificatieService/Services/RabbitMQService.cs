using VL.Solar.NotificatieService.Models;
using RabbitMQ.Client;
using System.Text;

namespace VL.Solar.NotificatieService.Services
{
    public class RabbitMQService
    {
        private readonly ConnectionFactory connectionFactory;
        private const string ExchangeName = "notifications";
        private string RoutingKey = "main";
        string[] berichtTypeLijst = { "Informatie", "Wijziging" };

        public RabbitMQService(ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void SendMessageToRabbitMQ(Notificatie? notificatie)
        {
            try
            {
                using (var connection = connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
                    
                    string message = ConverteerNotificationBericht(notificatie);

                    foreach (var type in berichtTypeLijst)
                    {
                        if (notificatie?.BerichtType == type)
                        {
                            RoutingKey = type;
                        }
                    }
                    channel.BasicPublish(ExchangeName, RoutingKey, null, Encoding.UTF8.GetBytes(message));
                    Console.WriteLine("Notification sent to RabbitMQ.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send notification to RabbitMQ: " + ex.Message);
            }
        }

        private string ConverteerNotificationBericht(Notificatie? notificatie)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(notificatie);
            return json;
        }
    }
}
