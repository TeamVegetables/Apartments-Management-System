using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AMS.Tests.AMS_Web_Tests
{

    [TestFixture]
    public class PaymentsControllerTests
    {
        private Mock<IPaymentService> paymentMock;
        private Mock<INotificationService> notificationMock;
        private Mock<IApartmentService> apartmentMock;
        private Mock<UserManager<User>> userManagerMock;
        private Mock<IUserStore<User>> userStoreMock;


        [SetUp]
        public void SetUp()
        {
            paymentMock = new Mock<IPaymentService>();
            notificationMock = new Mock<INotificationService>();
            apartmentMock = new Mock<IApartmentService>();
            userStoreMock = new Mock<IUserStore<User>>();
            userManagerMock =
                new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }


        [Test]
        public async Task GetApartmentAsync()
        {
            //Arrange
            var controller = new PaymentsController(paymentMock.Object, notificationMock.Object, apartmentMock.Object, userManagerMock.Object);

            //Act
            var actual = await controller.Index();
            //Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void ChangePayments()
        {
            //Arrange
            var controller = new PaymentsController(paymentMock.Object, notificationMock.Object, apartmentMock.Object, userManagerMock.Object);

            //Act
            var actual = controller.ChangePayments(1) as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }

        [Test]
        public void CreatePayment()
        {
            //Arrange
            var controller = new PaymentsController(paymentMock.Object, notificationMock.Object, apartmentMock.Object, userManagerMock.Object);

            //Act
            var actual = controller.CreatePayment(1) as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }
    }
}