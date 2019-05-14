using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Services;
using Moq;
using NUnit.Framework;

namespace AMS.Tests.Services
{
    [TestFixture]
    public class ApartmentServiceTests
    {
        private Mock<IUnitOfWork> uowMock;
        private Mock<IApartmentRepository> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            uowMock = new Mock<IUnitOfWork>();
            repositoryMock = new Mock<IApartmentRepository>();
            uowMock.Setup(u => u.Apartments).Returns(repositoryMock.Object);
        }

        [Test]
        public async Task GetApartmentAsync()
        {
            //Arrange
            var service = new ApartmentService(uowMock.Object);
            //Act
            await service.GetApartmentAsync(0);
            //Assert
            repositoryMock.Verify(r => r.GetAsync(It.Is<int>(v => v == 0)), Times.Once);
        }

        [Test]
        public void GetApartmentWithUsers()
        {
            //Arrange
            var service = new ApartmentService(uowMock.Object);
            //Act
            service.GetApartmentWithUsers(2);
            //Assert
            repositoryMock.Verify(r => r.GetWithUsers(It.Is<int>(v => v == 2)), Times.Once);
        }

        [Test]
        public void GetAllWithUsers()
        {
            //Arrange
            var service = new ApartmentService(uowMock.Object);
            //Act
            service.GetAllWithUsers();
            //Assert
            repositoryMock.Verify(r => r.GetAllWithUsers(), Times.Once);
        }

        [Test]
        public async Task GetAllApartmentsAsync()
        {
            //Arrange
            var service = new ApartmentService(uowMock.Object);
            //Act
            await service.GetAllApartmentsAsync();
            //Assert
            repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task AddApartmentAsync()
        {
            //Arrange
            var service = new ApartmentService(uowMock.Object);
            //Act
            await service.AddApartmentAsync(new Apartment());
            //Assert
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Apartment>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateApartmentAsync()
        {
            //Arrange
            var service = new ApartmentService(uowMock.Object);
            //Act
            await service.UpdateApartmentAsync(new Apartment());
            //Assert
            repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Apartment>()), Times.Once);
            uowMock.Verify(u => u.Save(), Times.Once);
        }

        [Test]
        public async Task RemoveApartmentAsync()
        {
            //Arrange
            var service = new ApartmentService(uowMock.Object);
            //Act
            await service.RemoveApartmentAsync(new Apartment());
            //Assert
            repositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Apartment>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
