using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AMS.Web.Models;
using System.Threading.Tasks;
using NUnit.Framework;
using AMS.Services.Interfaces;
using Moq;
using AMS.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace AMS.Tests.AMS.Web.Tests.Controllers.Test
{
    class ApartmentControllerTest
    {
        private Mock<IApartmentService> _service;
        private Mock<UserManager<User>> _manager;

        [Test]
        public void Initialize()
        {
            _service=new Mock<IApartmentService>();
        }


    }
}
