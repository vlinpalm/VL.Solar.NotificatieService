using Microsoft.AspNetCore.SignalR.Client;
using Moq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VL.Solar.SolarWorker.Services;
using Xunit;

namespace VL.Solar.SolarWorker.Tests
{
    public class NotificatieConsumerServiceTests
    {
        [Fact]
        public async Task ConsumeMessages_HubconnectionStartenEnBerichtenConsumeren()
        {
            // Arrange
            var connectionFactoryMock = new Mock<ConnectionFactory>();
            var connectionMock = new Mock<IConnection>();
            var channelMock = new Mock<IModel>();
            var hubConnectionMock = new Mock<HubConnection>();

            connectionFactoryMock.Setup(cf => cf.CreateConnection()).Returns(connectionMock.Object);
            connectionMock.Setup(c => c.CreateModel()).Returns(channelMock.Object);
            hubConnectionMock.Setup(hc => hc.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var service = new NotificatieConsumerService(connectionFactoryMock.Object, hubConnectionMock.Object);
            var queueName = "test-queue";

            // Act
            await service.ConsumeMessages(queueName);

            // Assert
            hubConnectionMock.Setup(hc => hc.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            channelMock.Verify(c => c.QueueDeclare(queueName, false, false, false, null), Times.Once);
            channelMock.Verify(c => c.BasicConsume(queueName, true, It.IsAny<EventingBasicConsumer>()), Times.Once);
        }
    }
}