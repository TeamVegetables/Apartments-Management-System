using AMS.Core.Models;
using AMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Tests.AMS.Core.Tests.Repositories.Tests
{
    [TestFixture]
    public class ApartmentRepositoryTest
    {


        private Mock<DbSet<Apartment>> dbSetMock;
        private Mock<ApplicationDbContext> dbContextMock;
        private List<User> users;
        private List<Apartment> apartments;

        [SetUp]
        public void SetUp()
        {
            Apartment apart1 = new Apartment{Id = 1};
        
            users = new List<User>
            {
                new User
                {
                    Id = "0",
                    ApartmentId = 1,
                    Apartment = apart1
                    
                },
                new User
                {
                    Id = "1",
                    ApartmentId = 1,
                    Apartment = apart1
                },
                new User
                {
                    Id = "2",
                    ApartmentId = 1,
                    Apartment = apart1
        }
            };

            apart1.Inhabitants = users;

            apartments = new List<Apartment>
            {
                apart1,
                new Apartment
                {
                    Id = 2
                },
                new Apartment
                {
                    Id = 3
                }
            };

            var queryable = apartments.AsQueryable();
            dbSetMock = new Mock<DbSet<Apartment>>();
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => apartments.FirstOrDefault(d => d.Id == (int)ids[0]));
            dbSetMock.As<IQueryable<Apartment>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<Apartment>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<Apartment>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<Apartment>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            dbContextMock.Setup(a => a.Apartments).Returns(dbSetMock.Object);

            //dbSetMock = new Mock<DbSet<Notification>>();
            //dbSetMock.Setup(m => m.Find(It.IsAny<object[]>()))
               // .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            //dbSetMock.As<IQueryable<Notification>>().Setup(m => m.Provider).Returns(queryable.Provider);
            //dbSetMock.As<IQueryable<Notification>>().Setup(m => m.Expression).Returns(queryable.Expression);
            //dbSetMock.As<IQueryable<Notification>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            //dbSetMock.As<IQueryable<Notification>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            //dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            //dbContextMock.Setup(a => a.Notifications).Returns(dbSetMock.Object);
        }
        [Test]
        public void GetAll_Calls_DbSet_Apartment()
        {
            //Arrange
            var repository = new ApartmentRepository(dbContextMock.Object);
            //Act
            var result = repository.GetAllWithUsers();
            //Assert
            dbContextMock.Verify(c => c.Apartments, Times.Once);
        }


    }
}

