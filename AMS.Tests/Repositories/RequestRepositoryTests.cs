using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AMS.Tests.Repositories
{

    [TestFixture]
    class RequestRepositoryTests
    {
        DbContextOptions<ApplicationDbContext> options;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;
        }

        [Test]
        public async Task AddAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var payment = new Request { Id = 1 };
                var repo = new RequestRepository(context);

                //Act
                await repo.AddAsync(payment);
                var expectedResult = 1;
                var actualResult = context.Requests.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task AddRangeAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var payments = new List<Request>
                {
                    new Request { Id = 1, },
                    new Request { Id = 2 },
                    new Request { Id = 3 }
                };
                var repo = new RequestRepository(context);

                //Act
                await repo.AddRangeAsync(payments);
                var expectedResult = 3;
                var actualResult = context.Requests.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task RemoveAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var payment = new Request { Id = 1 };
                var repo = new RequestRepository(context);

                //Act
                var expectedResult = 0;
                context.Requests.Add(payment);
                await repo.RemoveAsync(payment);
                var actualResult = context.Requests.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task RemoveRangeAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var payments = new List<Request>
                {
                    new Request { Id = 1, },
                    new Request { Id = 2 },
                    new Request { Id = 3 }
                };
                var repo = new RequestRepository(context);

                //Act
                var expectedResult = 0;
                await context.Requests.AddRangeAsync(payments);
                await repo.RemoveRangeAsync(payments);
                var actualResult = context.Requests.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task GetAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var payment = new Request { Id = 1 };
                var repo = new RequestRepository(context);

                //Act
                var expectedResult = payment;
                context.Requests.Add(payment);
                var result = await repo.GetAsync(payment.Id);

                //Assert
                Assert.AreEqual(expectedResult, result);
            }
        }

        [Test]
        public async Task GetAllAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var payments = new List<Request>
                {
                    new Request { Id = 1, },
                    new Request { Id = 2 },
                    new Request { Id = 3 }
                };
                var repo = new PaymentRepository(context);

                //Act
                foreach (var payment in payments)
                {
                    context.Requests.Add(payment);
                }
                var actualResult = await repo.GetAllAsync();
                var expectedResult = context.Requests;

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task GetRequestsByInhabitantIdTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {

                var user = new User {Id = "test1"};
               

                var payments = new List<Request>
                {
                    new Request { Id = 1, InitiatorId = user.Id},
                    new Request { Id = 2, InitiatorId = user.Id},
                    new Request { Id = 3, InitiatorId = user.Id}
                };
                var repo = new RequestRepository(context);

                //Act
                await repo.AddRangeAsync(payments);
                await repo.GetRequestsByInhabitantId(user.Id);
                var expectedResult = 3;
                var actualResult = context.Requests.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task GetRequestsByResolverIdTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {

                var user = new User { Id = "test1" };


                var payments = new List<Request>
                {
                    new Request { Id = 1, ResolverId = user.Id},
                    new Request { Id = 2, ResolverId = user.Id},
                    new Request { Id = 3, ResolverId = user.Id}
                };
                var repo = new RequestRepository(context);

                //Act
                await repo.AddRangeAsync(payments);
                await repo.GetRequestsByResolverId(user.Id);
                var expectedResult = 3;
                var actualResult = context.Requests.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
        [Test]
        public async Task UpdateAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var good = new Request { Id = 1 };
                var repo = new RequestRepository(context);

                //Act
                await repo.UpdateAsync(good);
                var expectedResult = EntityState.Modified;
                var actualResult = context.Entry(good).State;

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
    }
}
