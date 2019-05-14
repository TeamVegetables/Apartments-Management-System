using System;
using System.Threading.Tasks;
using NUnit.Framework;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Services;
using Moq;

namespace AMS.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private Mock<IUnitOfWork> uowMock;
        private Mock<IPaymentRepository> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            uowMock = new Mock<IUnitOfWork>();
            repositoryMock = new Mock<IPaymentRepository>();
            uowMock.Setup(u => u.Payments).Returns(repositoryMock.Object);
        }

        [Test]
        public async Task GetPaymentAsync()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            //Act
            await service.GetPaymentAsync(0);
            //Assert
            repositoryMock.Verify(r => r.GetAsync(It.Is<int>(v => v == 0)), Times.Once);
        }

        [Test]
        public async Task GetPaymentsByApartment()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            //Act
            await service.GetPaymentsByApartment(0);
            //Assert
            repositoryMock.Verify(r => r.GetPaymentsByApartment(It.Is<int>(v => v == 0)), Times.Once);
        }

        [Test]
        public async Task GetAllPaymentsAsync()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            //Act
            await service.GetAllPaymentsAsync();
            //Assert
            repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task AddPaymentAsync()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            //Act
            await service.AddPaymentAsync(new Payment());
            //Assert
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Payment>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task UpdatePaymentAsync()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            //Act
            await service.UpdatePaymentAsync(new Payment());
            //Assert
            repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Payment>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task RemovePaymentAsync()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            //Act
            await service.RemovePaymentAsync(new Payment());
            //Assert
            repositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Payment>()), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task ChangeStatus()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            var payment = new Payment
            {
                Completed = DateTime.Now,
                Status = PaymentStatus.Waiting
            };
            //Act
            await service.ChangeStatus(payment);
            //Assert
            repositoryMock.Verify(r => r.UpdateAsync(It.Is<Payment>(p => p.Status == PaymentStatus.NotConfirmPaid)), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }

        [Test]
        public async Task ChangeOverdueStatus()
        {
            //Arrange
            var service = new PaymentService(uowMock.Object);
            var payment = new Payment
            {
                Status = PaymentStatus.Waiting,
                DeadLine = DateTime.MinValue
            };
            //Act
            await service.ChangeStatus(payment);
            //Assert
            repositoryMock.Verify(r => r.UpdateAsync(It.Is<Payment>(p => p.Status == PaymentStatus.Overdue)), Times.Once);
            uowMock.Verify(u => u.SaveAsync());
        }
    }
}
