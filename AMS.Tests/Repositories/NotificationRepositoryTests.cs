using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using AMS.Core.Models;
using AMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace AMS.Tests.Repositories
{
    [TestFixture]
    public class NotificationRepositoryTests
    {
        private Mock<DbSet<Notification>> dbSetMock;
        private Mock<ApplicationDbContext> dbContextMock;
        private List<Notification> data;
        DbContextOptions<ApplicationDbContext> options;
        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;
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
                    Dismissed = false,
                    Message = "test2",
                    UserId = "test"
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
        public async Task AddAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var notification = new Notification { Id = 1 };
                var repo = new NotificationRepository(context);

                //Act
                await repo.AddAsync(notification);
                var expectedResult = 1;
                var actualResult = context.Notifications.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task AddRangeAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var notifications = new List<Notification>
                {
                    new Notification { Id = 1, },
                    new Notification { Id = 2 },
                    new Notification { Id = 3 }
                };
                var repo = new NotificationRepository(context);

                //Act
                await repo.AddRangeAsync(notifications);
                var expectedResult = 3;
                var actualResult = context.Notifications.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task RemoveAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var notification = new Notification { Id = 1 };
                var repo = new NotificationRepository(context);

                //Act
                var expectedResult = 0;
                context.Notifications.Add(notification);
                await repo.RemoveAsync(notification);
                var actualResult = context.Notifications.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task RemoveRangeAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var notifications = new List<Notification>
                {
                    new Notification { Id = 1, },
                    new Notification { Id = 2 },
                    new Notification { Id = 3 }
                };
                var repo = new NotificationRepository(context);

                //Act
                var expectedResult = 0;
                await context.Notifications.AddRangeAsync(notifications);
                await repo.RemoveRangeAsync(notifications);
                var actualResult = context.Notifications.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task GetAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var notification = new Notification { Id = 1 };
                var repo = new NotificationRepository(context);

                //Act
                var expectedResult = notification;
                context.Notifications.Add(notification);
                var result = await repo.GetAsync(notification.Id);

                //Assert
                Assert.AreEqual(expectedResult, result);
            }
        }

        [Test]
        public async Task GetAllAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var notifications = new List<Notification>
                {
                    new Notification { Id = 1, },
                    new Notification { Id = 2 },
                    new Notification { Id = 3 }
                };
                var repo = new NotificationRepository(context);

                //Act
                foreach (var notification in notifications)
                {
                    context.Notifications.Add(notification);
                }
                var actualResult = await repo.GetAllAsync();
                var expectedResult = context.Notifications;

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task UpdateAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var notification = new Notification { Id = 1 };
                var repo = new NotificationRepository(context);

                //Act
                await repo.UpdateAsync(notification);
                var expectedResult = EntityState.Modified;
                var actualResult = context.Entry(notification).State;

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
    

    [Test]
        public void GetAll_Calls_DbSet_Notifications()
        {
            //Arrange
            var repository = new NotificationRepository(dbContextMock.Object);
            //Act
            repository.GetAll();
            //Assert
            dbContextMock.Verify(c => c.Notifications, Times.Once);
        }

        [Test]
        public async Task Create_Calls_DbSet_AddAsync()
        {
            //Arrange
            var repository = new NotificationRepository(dbContextMock.Object);
            dbSetMock.Setup(c => c.AddAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()))
                .Callback((Notification model, CancellationToken token) => { data.Add(model); })
                .Returns((Notification model, CancellationToken token) =>
                    Task.FromResult((EntityEntry<Notification>) null));
            dbContextMock.Setup(c => c.Set<Notification>()).Returns(dbSetMock.Object);
            //Act
            var notification = new Notification
            {
                Id = 3,
                Date = DateTime.Now,
                Dismissed = true,
                Message = "test3",
                UserId = Guid.NewGuid().ToString()
            };
            await repository.Create(notification);
            //Assert
            dbSetMock.Verify(c => c.AddAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Get_Calls_Find()
        {
            //Arrange
            var repository = new NotificationRepository(dbContextMock.Object);
            var notification = new Notification
            {
                Id = 3,
                Date = DateTime.Now,
                Dismissed = true,
                Message = "test3",
                UserId = Guid.NewGuid().ToString()
            };
            dbSetMock.Setup(c => c.Find(It.IsAny<int>())).Returns(notification);
            //Act
            var result = repository.Get(3);
            //Assert
            dbSetMock.Verify(c => c.Find(It.Is<int>(i => i == 3)), Times.Once);
            Assert.AreEqual(notification.Id, result.Id);
        }

        [Test]
        public void Get_By_User_Id()
        {
            //Arrange
            var repository = new NotificationRepository(dbContextMock.Object);
            //Act
            var result = repository.GetByUserId("test");
            //Assert
            dbContextMock.Verify(c => c.Notifications, Times.Once);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result.FirstOrDefault().Id);
        }
    }
}
