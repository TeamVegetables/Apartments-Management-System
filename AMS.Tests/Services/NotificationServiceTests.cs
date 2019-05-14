using System.Threading.Tasks;
using NUnit.Framework;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Services;
using Moq;

namespace AMS.Tests.Services
{
    [TestFixture]
    public class NotificationServiceTests
    {
        private Mock<IUnitOfWork> uowMock;
        private Mock<INotificationRepository> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            uowMock = new Mock<IUnitOfWork>();
            repositoryMock = new Mock<INotificationRepository>();
            uowMock.Setup(u => u.Notifications).Returns(repositoryMock.Object);
        }

        [Test]
        public async Task Create()
        {
            //Arrange
            var service = new NotificationService(uowMock.Object);
            //Act
            await service.Create("test","test");
            //Assert
            repositoryMock.Verify(r => r.Create(It.IsAny<Notification>()), Times.Once);
        }

        [Test]
        public void GetByUserId()
        {
            //Arrange
            var service = new NotificationService(uowMock.Object);
            //Act
            service.GetByUserId("test");
            //Assert
            repositoryMock.Verify(r => r.GetByUserId(It.Is<string>(s => s == "test")), Times.Once);
        }

        [Test]
        public void Dismiss()
        {
            //Arrange
            var service = new NotificationService(uowMock.Object);
            repositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(new Notification());
            //Act
            service.Dismiss(0);
            //Assert
            repositoryMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Notification>()), Times.Once);
            uowMock.Verify(u => u.Save(), Times.Once);
        }
    }
}
