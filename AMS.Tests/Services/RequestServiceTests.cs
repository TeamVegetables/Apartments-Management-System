using System.Threading.Tasks;
using NUnit.Framework;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Services;
using Moq;

namespace AMS.Tests.Services
{
    [TestFixture]
    public class RequestServiceTests
    {
        private Mock<IUnitOfWork> uowMock;
        private Mock<IRequestRepository> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            uowMock = new Mock<IUnitOfWork>();
            repositoryMock = new Mock<IRequestRepository>();
            uowMock.Setup(u => u.Requests).Returns(repositoryMock.Object);
        }

        [Test]
        public async Task GetRequestAsync()
        {
            //Arrange
            var service = new RequestService(uowMock.Object);
            //Act
            await service.GetRequestAsync(0);
            //Assert
            repositoryMock.Verify(r => r.GetAsync(It.Is<int>(v => v == 0)), Times.Once);
        }

        [Test]
        public async Task GetAllRequestsAsync()
        {
            //Arrange
            var service = new RequestService(uowMock.Object);
            //Act
            await service.GetAllRequestsAsync();
            //Assert
            repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetRequestsByInhabitantId()
        {
            //Arrange
            var service = new RequestService(uowMock.Object);
            //Act
            await service.GetRequestsByInhabitantId("test");
            //Assert
            repositoryMock.Verify(r => r.GetRequestsByInhabitantId(It.Is<string>(v => v == "test")), Times.Once);
        }

        [Test]
        public async Task GetRequestsByResolverId()
        {
            //Arrange
            var service = new RequestService(uowMock.Object);
            //Act
            await service.GetRequestsByResolverId("test");
            //Assert
            repositoryMock.Verify(r => r.GetRequestsByResolverId(It.Is<string>(v => v == "test")), Times.Once);
        }

        [Test]
        public async Task AddRequestAsync()
        {
            //Arrange
            var service = new RequestService(uowMock.Object);
            //Act
            await service.AddRequestAsync(new Request());
            //Assert
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Request>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task UpdateRequestAsync()
        {
            //Arrange
            var service = new RequestService(uowMock.Object);
            //Act
            await service.UpdateRequestAsync(new Request());
            //Assert
            repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Request>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task RemoveRequestAsync()
        {
            //Arrange
            var service = new RequestService(uowMock.Object);
            //Act
            await service.RemoveRequestAsync(new Request());
            //Assert
            repositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Request>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }
    }
}
