// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using VL.Solar.NotificatieService.Controllers;
// using VL.Solar.NotificatieService.Models;
// using VL.Solar.NotificatieService.Models.Data;
// using VL.Solar.NotificatieService.Services.Interfaces;
// using Xunit;
//
// namespace VL.Solar.NotificatieService.Tests;
//
//     [Collection("Sequential")]
//     public class MedewerkerNotificatieControllerTests
//     {
//         [Fact]
//         public async void CreateMedewerkerNotificatie_ReturnsOk()
//         {
//             // Arrange
//             var mockService = new Mock<IMedewerkerNotificatieService>();
//             var controller = new MedewerkerNotificatieController(mockService.Object);
//             var createMedewerkerNotificatie = new CreateMedewerkerNotificatie();
//
//             // Act
//             var result = controller.CreateMedewerkerNotificatie(createMedewerkerNotificatie);
//
//             // Assert
//             Assert.IsType<OkResult>(result);
//         }
//         
//         [Fact]
//         public async void GetMedewerkerNotificaties_ReturnsOkMedewerkerNotificaties()
//         {
//             // Arrange
//             var mockService = new Mock<IMedewerkerNotificatieService>();
//             var controller = new MedewerkerNotificatieController(mockService.Object);
//             var expectedNotificaties = new List<MedewerkerNotificatie>();
//
//             mockService.Setup(r => r.GetMedewerkerNotificaties())
//                 .Returns(expectedNotificaties);
//
//             // Act
//             var result = controller.GetMedewerkerNotificaties();
//
//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             var actualNotificaties = Assert.IsAssignableFrom<List<MedewerkerNotificatie>>(okResult.Value);
//             Assert.Equal(expectedNotificaties, actualNotificaties);
//         }
//
//         [Fact]
//         public void GetMedewerkerNotificatieById_Bestaat()
//         {
//             // Arrange
//             var mockService = new Mock<IMedewerkerNotificatieService>();
//             var controller = new MedewerkerNotificatieController(mockService.Object);
//             var expectedNotificatie = new MedewerkerNotificatie { MedewerkerNotificationId = 1 };
//
//             mockService.Setup(r => r.GetMedewerkerNotificatieById(It.IsAny<int>()))
//                 .Returns(expectedNotificatie);
//
//             // Act
//             var result = controller.GetMedewerkerNotificatieById(1);
//
//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             var actualNotificatie = Assert.IsAssignableFrom<MedewerkerNotificatie>(okResult.Value);
//             Assert.Equal(expectedNotificatie, actualNotificatie);
//         }
//
//         [Fact]
//         public void GetMedewerkerNotificatieById_IdBestaatNiet()
//         {
//             // Arrange
//             var mockService = new Mock<IMedewerkerNotificatieService>();
//             var controller = new MedewerkerNotificatieController(mockService.Object);
//             mockService.Setup(r => r.GetMedewerkerNotificatieById(It.IsAny<int>()))
//                 .Returns((MedewerkerNotificatie)null);
//
//             // Act
//             var result = controller.GetMedewerkerNotificatieById(1);
//
//             // Assert
//             Assert.IsType<NotFoundResult>(result);
//         }
//
//     }