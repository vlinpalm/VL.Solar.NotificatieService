using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using VL.Solar.NotificatieService.Data;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Repositories;

namespace VL.Solar.NotificatieService.Tests
{
    public class NotificatieRepositoryTests
    {
        [Fact]
        public void CreateNotificatie_ShouldAddNotificatieToDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var repository = new NotificatieRepository(dbContextMock.Object);
            var notificatie = new Notificatie();

            // Act
            repository.CreateNotificatie(notificatie);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            dbContextMock.Verify(db => db.Add(notificatie), Times.Once);
        }

        [Fact]
        public void GetNotificaties_ShouldReturnAllNotificatiesFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var repository = new NotificatieRepository(dbContextMock.Object);
            var expectedNotificaties = new List<Notificatie> { new Notificatie(), new Notificatie() };
            var dbSetMock = CreateDbSetMock(expectedNotificaties);

            dbContextMock.Setup(db => db.Notificaties).Returns(dbSetMock.Object);

            // Act
            var result = repository.GetNotificaties();

            // Assert
            Assert.Equal(expectedNotificaties, result);
        }

        [Fact]
        public void GetNotificatieById_ShouldReturnCorrectNotificatieFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var repository = new NotificatieRepository(dbContextMock.Object);
            var expectedNotificatie = new Notificatie { NotificatieId = 1 };
            var dbSetMock = CreateDbSetMock(new List<Notificatie> { expectedNotificatie });

            dbContextMock.Setup(db => db.Notificaties).Returns(dbSetMock.Object);

            // Act
            var result = repository.GetNotificatieById(1);

            // Assert
            Assert.Equal(expectedNotificatie, result);
        }

        [Fact]
        public void UpdateNotificatie_ShouldUpdateNotificatieInDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var repository = new NotificatieRepository(dbContextMock.Object);
            var notificatie = new Notificatie { NotificatieId = 1 };

            // Act
            repository.UpdateNotificatie(notificatie);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            dbContextMock.Verify(db => db.Update(notificatie), Times.Once);
        }

        [Fact]
        public void GetNotificatiesByTeamNaam_ShouldReturnNotificatiesWithMatchingTeamNaamFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var repository = new NotificatieRepository(dbContextMock.Object);
            var expectedNotificaties = new List<Notificatie> { new Notificatie { TeamNaam = "Team A" }, new Notificatie { TeamNaam = "Team B" } };
            var dbSetMock = CreateDbSetMock(expectedNotificaties);

            dbContextMock.Setup(db => db.Notificaties).Returns(dbSetMock.Object);

            // Act
            var result = repository.GetNotificatiesByTeamNaam("Team A");

            // Assert
            Assert.Equal(expectedNotificaties, result);
        }

        [Fact]
        public void DeleteNotificatie_ShouldRemoveNotificatieFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var repository = new NotificatieRepository(dbContextMock.Object);
            var notificatieId = 1;
            var notificatie = new Notificatie { NotificatieId = notificatieId };
            var dbSetMock = CreateDbSetMock(new List<Notificatie> { notificatie });

            dbContextMock.Setup(db => db.Notificaties).Returns(dbSetMock.Object);

            // Act
            repository.DeleteNotificatie(notificatieId);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(notificatie), Times.Once);
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return dbSetMock;
        }
    }
}
