﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using VL.Solar.NotificatieService.Data;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;
using VL.Solar.NotificatieService.Repositories;

namespace VL.Solar.NotificatieService.Tests
{
    public class MedewerkerNotificatieRepositoryTests
    {
        [Fact]
        public void GetMedewerkerNotificaties_ShouldReturnAllMedewerkerNotificatiesFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var mapperMock = new Mock<IMapper>();
            var repository = new MedewerkerNotificatieRepository(dbContextMock.Object, mapperMock.Object);
            var expectedNotificaties = new List<MedewerkerNotificatie> { new MedewerkerNotificatie(), new MedewerkerNotificatie() };
            var dbSetMock = CreateDbSetMock(expectedNotificaties);

            dbContextMock.Setup(db => db.MedewerkerNotificaties).Returns(dbSetMock);

            // Act
            var result = repository.GetMedewerkerNotificaties();

            // Assert
            Assert.Equal(expectedNotificaties, result);
        }

        [Fact]
        public void GetMedewerkerNotificatieById_ShouldReturnCorrectMedewerkerNotificatieFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var mapperMock = new Mock<IMapper>();
            var repository = new MedewerkerNotificatieRepository(dbContextMock.Object, mapperMock.Object);
            var expectedNotificatie = new MedewerkerNotificatie { MedewerkerNotificationId = 1 };
            var dbSetMock = CreateDbSetMock(new List<MedewerkerNotificatie> { expectedNotificatie });

            dbContextMock.Setup(db => db.MedewerkerNotificaties).Returns(dbSetMock);


            // Act
            var result = repository.GetMedewerkerNotificatieById(1);

            // Assert
            Assert.Equal(expectedNotificatie, result);
        }

        [Fact]
        public void GetMedewerkerNotificatiesByMedewerker_ShouldReturnNotificatiesWithMatchingMedewerkerIdFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var mapperMock = new Mock<IMapper>();
            var repository = new MedewerkerNotificatieRepository(dbContextMock.Object, mapperMock.Object);
            var expectedNotificaties = new List<MedewerkerNotificatie> { new MedewerkerNotificatie { MedewerkerId = "User123" }, new MedewerkerNotificatie { MedewerkerId = "User123" } };
            var dbSetMock = CreateDbSetMock(expectedNotificaties);

            dbContextMock.Setup(db => db.MedewerkerNotificaties).Returns(dbSetMock);


            // Act
            var result = repository.GetMedewerkerNotificatiesByMedewerker("User123");

            // Assert
            Assert.Equal(expectedNotificaties, result);
        }

        [Fact]
        public void GetMedewerkerNotificatiesByNotificatieId_ShouldReturnNotificatiesWithMatchingNotificatieIdFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var mapperMock = new Mock<IMapper>();
            var repository = new MedewerkerNotificatieRepository(dbContextMock.Object, mapperMock.Object);
            var expectedNotificaties = new List<MedewerkerNotificatie> { new MedewerkerNotificatie { NotificatieId = 1 }, new MedewerkerNotificatie { NotificatieId = 1 } };
            var dbSetMock = CreateDbSetMock(expectedNotificaties);

            dbContextMock.Setup(db => db.MedewerkerNotificaties).Returns(dbSetMock);


            // Act
            var result = repository.GetMedewerkerNotificatiesByNotificatieId(1);

            // Assert
            Assert.Equal(expectedNotificaties, result);
        }

        [Fact]
        public void CreateMedewerkerNotificatie_ShouldAddNotificatieToDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var mapperMock = new Mock<IMapper>();
            var repository = new MedewerkerNotificatieRepository(dbContextMock.Object, mapperMock.Object);
            var createMedewerkerNotificatie = new CreateMedewerkerNotificatie();
            var medewerkerNotificatie = new MedewerkerNotificatie();
            
            mapperMock.Setup(mapper => mapper.Map<MedewerkerNotificatie>(createMedewerkerNotificatie))
                .Returns(medewerkerNotificatie);

            // Act
            repository.CreateMedewerkerNotificatie(createMedewerkerNotificatie);

            // Assert
            dbContextMock.Verify(db => db.MedewerkerNotificaties.Add(medewerkerNotificatie), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdateMedewerkerNotificatie_ShouldUpdateMedewerkerNotificatieInDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var mapperMock = new Mock<IMapper>();
            var repository = new MedewerkerNotificatieRepository(dbContextMock.Object, mapperMock.Object);
            var medewerkerNotificatieId = 1;
            var medewerkerNotificatie = new MedewerkerNotificatie { MedewerkerNotificationId = medewerkerNotificatieId };
            var dbSetMock = CreateDbSetMock(new List<MedewerkerNotificatie> { medewerkerNotificatie });

            dbContextMock.Setup(db => db.MedewerkerNotificaties).Returns(dbSetMock);


            // Act
            repository.UpdateMedewerkerNotificatie(medewerkerNotificatieId, medewerkerNotificatie);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            Assert.True(medewerkerNotificatie.Gelezen);
        }

        [Fact]
        public void GetUnreadNotificaties_ShouldReturnUnreadNotificatiesFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var mapperMock = new Mock<IMapper>();
            var repository = new MedewerkerNotificatieRepository(dbContextMock.Object, mapperMock.Object);
            var expectedNotificaties = new List<MedewerkerNotificatie> { new MedewerkerNotificatie { Gelezen = false }, new MedewerkerNotificatie { Gelezen = false } };
            var dbSetMock = CreateDbSetMock(expectedNotificaties);

            dbContextMock.Setup(db => db.MedewerkerNotificaties).Returns(dbSetMock);


            // Act
            var result = repository.GetUnreadNotificaties();

            // Assert
            Assert.Equal(expectedNotificaties, result);
        }

        private static DbSet<T> CreateDbSetMock<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            return dbSetMock.Object;
        }
    }
}
