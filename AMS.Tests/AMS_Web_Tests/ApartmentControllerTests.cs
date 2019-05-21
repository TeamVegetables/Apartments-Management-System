﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.Controllers;
using AMS.Web.ViewModels.Apartments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AMS.Tests.AMS_Web_Tests
{
    [TestFixture]
    public class ApartmentControllerTests
    {
        private Mock<IApartmentService> apartmentMock;
        private Mock<UserManager<User>> userManagerMock;
        private Mock<IUserStore<User>> userStoreMock;

        [SetUp]
        public void SetUp()
        {
            apartmentMock = new Mock<IApartmentService>();
            userStoreMock = new Mock<IUserStore<User>>();
            userManagerMock =
                new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }


        [Test]
        public async Task GetApartmentAsync()
        {
            //Arrange
            var controller = new ApartmentsController(apartmentMock.Object, userManagerMock.Object);

            //Act
            await controller.Index();
            //Assert
            apartmentMock.Verify(r => r.GetAllApartmentsAsync(), Times.Once);
        }

        [Test]
        public void CreateApartment()
        {
            //Arrange
            var controller = new ApartmentsController(apartmentMock.Object, userManagerMock.Object);

            //Act
            var actual = controller.CreateApartment() as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }
     
        [Test]
        public void RecentlyFreeApartments()
        {
            //Arrange
            var controller = new ApartmentsController(apartmentMock.Object, userManagerMock.Object);

            //Act
            var actual =  controller.RecentlyFreeApartments() as ViewResult;
            //Assert

             Assert.IsNotNull(actual);
        }

        [Test]
        public void ManageInhabitants()
        {
            //Arrange
            var controller = new ApartmentsController(apartmentMock.Object, userManagerMock.Object);

            //Act
            var actual = controller.ManageInhabitants(1) as ViewResult;
            //Assert

            Assert.IsNotNull(actual);
        }

        //[Test]
        //public async Task GetApartmentByInhabitantId()
        //{
        //    //Arrange
        //    userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
        //        .Returns(() => new Task<User>(() => new User{ApartmentId = 1}));
        //    var controller = new ApartmentsController(apartmentMock.Object, userManagerMock.Object);

        //    //Act
        //    var actual = await controller.GetApartmentByInhabitantId() as ViewResult;
        //    //Assert

        //    Assert.IsNotNull(actual);
        //}
    }
}