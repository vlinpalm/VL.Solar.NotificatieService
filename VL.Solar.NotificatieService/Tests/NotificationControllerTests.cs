using Microsoft.AspNetCore.Mvc;
using Moq;
using VL.Solar.NotificatieService.Controllers;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Repositories;
using VL.Solar.NotificatieService.Services.Interfaces;
using Xunit;

namespace VL.Solar.NotificatieService.Tests
{
    [Collection("Sequential")]
    public class NotificatieControllerTests
    {
        [Fact]
        public void CreateNotificatie_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);
            var notificatie = new Notificatie();

            // Act
            var result = controller.CreateNotificatie(notificatie);

            // Assert
            Assert.IsType<OkResult>(result);
            mockService.Verify(r => r.CreateNotificatie(notificatie), Times.Once);
            auditLogServiceMock.Verify(a => a.LogAuditEvent("CreateNotificatie", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetNotificaties_ReturnsOkNotificaties()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);
            var expectedNotificaties = new List<Notificatie>();

            mockService.Setup(r => r.GetNotificaties())
                .Returns(expectedNotificaties);

            // Act
            var result = controller.GetNotificaties();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualNotificaties = Assert.IsAssignableFrom<List<Notificatie>>(okResult.Value);
            Assert.Equal(expectedNotificaties, actualNotificaties);
            mockService.Verify(r => r.GetNotificaties(), Times.Once);
            auditLogServiceMock.Verify(a => a.LogAuditEvent("GetNotificaties", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetNotificatieById_ExistingId_ReturnsOkNotificatie()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);
            var expectedNotificatie = new Notificatie { NotificatieId = 1 };

            mockService.Setup(r => r.GetNotificatieById(It.IsAny<int>()))
                .Returns(expectedNotificatie);

            // Act
            var result = controller.GetNotificatieById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualNotificatie = Assert.IsAssignableFrom<Notificatie>(okResult.Value);
            Assert.Equal(expectedNotificatie, actualNotificatie);
            mockService.Verify(r => r.GetNotificatieById(1), Times.Once);
            auditLogServiceMock.Verify(a => a.LogAuditEvent("GetNotificatieById", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetNotificatieById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);

            mockService.Setup(s => s.GetNotificatieById(It.IsAny<int>()))
                .Returns((Notificatie)null);

            // Act
            var result = controller.GetNotificatieById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockService.Verify(s => s.GetNotificatieById(1), Times.Once);
        }


        [Fact]
        public void UpdateNotificatie_ExistingId_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);
            var existingNotificatie = new Notificatie { NotificatieId = 1 };
            var updatedNotificatie = new Notificatie { NotificatieId = 1, BerichtType = "Updated" };

            mockService.Setup(r => r.GetNotificatieById(It.IsAny<int>()))
                .Returns(existingNotificatie);

            // Act
            var result = controller.UpdateNotificatie(1, updatedNotificatie);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.Equal(updatedNotificatie.BerichtType, existingNotificatie.BerichtType);
            mockService.Verify(r => r.UpdateNotificatie(existingNotificatie), Times.Once);
            auditLogServiceMock.Verify(a => a.LogAuditEvent("UpdateNotificatie", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void UpdateNotificatie_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);
            var updatedNotificatie = new Notificatie { NotificatieId = 1, BerichtType = "Updated" };

            mockService.Setup(r => r.GetNotificatieById(It.IsAny<int>()))
                .Returns((Notificatie)null);

            // Act
            var result = controller.UpdateNotificatie(1, updatedNotificatie);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockService.Verify(r => r.UpdateNotificatie(It.IsAny<Notificatie>()), Times.Never);
            auditLogServiceMock.Verify(a => a.LogAuditEvent(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void DeleteNotificatie_ExistingId_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);
            var existingNotificatie = new Notificatie { NotificatieId = 1 };

            mockService.Setup(r => r.GetNotificatieById(It.IsAny<int>()))
                .Returns(existingNotificatie);

            // Act
            var result = controller.DeleteNotificatie(1);

            // Assert
            Assert.IsType<OkResult>(result);
            mockService.Verify(r => r.DeleteNotificatie(1), Times.Once);
            auditLogServiceMock.Verify(a => a.LogAuditEvent("DeleteNotificatie", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteNotificatie_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<INotificatieService>();
            var auditLogServiceMock = new Mock<IAuditLogService>();
            var controller = new NotificatieController(mockService.Object);

            mockService.Setup(r => r.GetNotificatieById(It.IsAny<int>()))
                .Returns((Notificatie)null);

            // Act
            var result = controller.DeleteNotificatie(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockService.Verify(r => r.DeleteNotificatie(It.IsAny<int>()), Times.Never);
            auditLogServiceMock.Verify(a => a.LogAuditEvent(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
