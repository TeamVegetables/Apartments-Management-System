using System.Security.Claims;
using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.Controllers;
using AMS.Web.ViewModels.Request;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AMS.Tests.AMS_Web_Tests
{

    [TestFixture]
    public class RequestsControllerTests
    {
        private Mock<IRequestService> requestMock;
        private Mock<UserManager<User>> userManagerMock;
        private Mock<IUserStore<User>> userStoreMock;
        private Mock<IMapper> mapper;

        [SetUp]
        public void SetUp()
        {
            requestMock = new Mock<IRequestService>();
            userStoreMock = new Mock<IUserStore<User>>();
            mapper = new Mock<IMapper>();
            userManagerMock =
                new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }


        [Test]
        public void Index()
        {
            //Arrange
            var controller = new RequestsController(requestMock.Object, userManagerMock.Object, mapper.Object);

            //Act
            var actual = controller.Index() as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }

        [Test]
        public void CreateRequest()
        {
            //Arrange
            var controller = new RequestsController(requestMock.Object, userManagerMock.Object, mapper.Object);

            //Act
            var actual = controller.CreateRequest() as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task UpdateRequestStatus_WithNotFoundInDb()
        {
            //Arrange
            var controller = new RequestsController(requestMock.Object, userManagerMock.Object, mapper.Object);

            //Act
            var actual = await controller.UpdateRequestStatus(new UpdateRequestStatusViewModel
            {
                Id = 1,
                Status = RequestStatus.InProgress,
                UserId = "test-test-test-test"
            }) as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task UpdateRequestStatus_Success()
        {
            //Arrange
            requestMock.Setup(r => r.GetRequestAsync(It.IsAny<int>()))
                .Returns(() => new Task<Request>(() => new Request()));
            var controller = new RequestsController(requestMock.Object, userManagerMock.Object, mapper.Object);

            //Act
            var actual = await controller.UpdateRequestStatus(new UpdateRequestStatusViewModel
            {
                Id = 1,
                Status = RequestStatus.InProgress,
                UserId = "test-test-test-test"
            }) as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }
    }
}
    