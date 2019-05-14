using NUnit.Framework;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Services;
using Moq;

namespace AMS.Tests.Services
{
    [TestFixture]
    public class RentInfosServiceTests
    {
        private Mock<IUnitOfWork> uowMock;
        private Mock<IRentInfoRepository> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            uowMock = new Mock<IUnitOfWork>();
            repositoryMock = new Mock<IRentInfoRepository>();
            uowMock.Setup(u => u.RentInfos).Returns(repositoryMock.Object);
        }

        [Test]
        public async Task GetRentInfoAsync()
        {
            //Arrange
            var service = new RentInfoService(uowMock.Object);
            //Act
            await service.GetRentInfoAsync(0);
            //Assert
            repositoryMock.Verify(r => r.GetAsync(It.Is<int>(v => v == 0)), Times.Once);
        }

        [Test]
        public async Task GetAllRentInfosAsync()
        {
            //Arrange
            var service = new RentInfoService(uowMock.Object);
            //Act
            await service.GetAllRentInfosAsync();
            //Assert
            repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task AddRentInfoAsync()
        {
            //Arrange
            var service = new RentInfoService(uowMock.Object);
            //Act
            await service.AddRentInfoAsync(new RentInfo());
            //Assert
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<RentInfo>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task UpdateRentInfoAsync()
        {
            //Arrange
            var service = new RentInfoService(uowMock.Object);
            //Act
            await service.UpdateRentInfoAsync(new RentInfo());
            //Assert
            repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<RentInfo>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task RemoveRentInfoAsync()
        {
            //Arrange
            var service = new RentInfoService(uowMock.Object);
            //Act
            await service.RemoveRentInfoAsync(new RentInfo());
            //Assert
            repositoryMock.Verify(r => r.RemoveAsync(It.IsAny<RentInfo>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }
    }
}
