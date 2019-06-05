using NUnit.Framework;
using AMS.Core.Interfaces;
using AMS.Services.Services;
using Moq;

namespace AMS.Tests.Services
{
    [TestFixture]
    public class BaseServiceTests
    {
        private Mock<IUnitOfWork> uowMock;

        [SetUp]
        public void SetUp()
        {
            uowMock = new Mock<IUnitOfWork>();
        }

        //[Test]
        //public void Dispose()
        //{
        //    //Arrange
        //    var service = new BaseService(uowMock.Object);
        //    //Act
        //    service.Dispose();
        //    //Assert
        //    uowMock.Verify(u => u.Dispose());
        //}
    }
}
