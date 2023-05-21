using Moq;
using RabbitMQ.Client;
using Xunit;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Services;

namespace VL.Solar.NotificatieService.Tests
{
    public class RabbitMQServiceTests
    {
        [Fact]
        public void SendMessageToRabbitMQ_ShouldSendNotificationToRabbitMQExchange()
        {
            // Arrange
            var connectionFactoryMock = new Mock<ConnectionFactory>();
            var connectionMock = new Mock<IConnection>();
            var channelMock = new Mock<IModel>();

            connectionFactoryMock.Setup(cf => cf.CreateConnection()).Returns(connectionMock.Object);
            connectionMock.Setup(c => c.CreateModel()).Returns(channelMock.Object);

            var service = new RabbitMQService(connectionFactoryMock.Object);
            var notificatie = new Notificatie();

            // Act
            service.SendMessageToRabbitMQ(notificatie);

            // Assert
            channelMock.Verify(c => c.ExchangeDeclare("notifications", ExchangeType.Direct, true, false, null), Times.Once);
            channelMock.Verify(c => c.BasicPublish("notifications", It.IsAny<string>(), null, It.IsAny<byte[]>()), Times.Once);
        }
    }
}