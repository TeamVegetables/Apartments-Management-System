using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AMS.Tests.Repositories
{
    [TestFixture]
    public class NotificationRepositoryTests
    {
        private Mock<DbSet<Notification>> dbSetMock;
        private Mock<ApplicationDbContext> dbContextMock;
        private List<Notification> data;

        [SetUp]
        public void SetUp()
        {
            data = new List<Notification>
            {
                new Notification
                {
                    Id = 0,
                    Date = DateTime.Now,
                    Dismissed = false,
                    Message = "test",
                    UserId = Guid.NewGuid().ToString()
                },
                new Notification
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Dismissed = false,
                    Message = "test1",
                    UserId = Guid.NewGuid().ToString()
                },
                new Notification
                {
                    Id = 2,
                    Date = DateTime.Now,
                    Dismissed = true,
                    Message = "test2",
                    UserId = Guid.NewGuid().ToString()
                }
            };
            var queryable = data.AsQueryable();
            dbSetMock = new Mock<DbSet<Notification>>();
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            dbSetMock.As<IQueryable<Notification>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<Notification>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<Notification>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<Notification>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            dbContextMock.Setup(a => a.Notifications).Returns(dbSetMock.Object);
        }

        [Test]
        public void GetAll_Calls_DbSet_Notifications()
        {
            //Arrange
            var repository = new NotificationRepository(dbContextMock.Object);
            //Act
            var result = repository.GetAll();
            //Assert
            dbContextMock.Verify(c => c.Notifications, Times.Once);
        }
    }
}
