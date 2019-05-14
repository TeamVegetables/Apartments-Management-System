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
    public class RentInfoRepositoryTests
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
                var payment = new RentInfo {Id = 1};
                var repo = new RentInfoRepository(context);

                //Act
                await repo.AddAsync(payment);
                var expectedResult = 1;
                var actualResult = context.RentInfos.Local.Count();

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
                var payments = new List<RentInfo>
                {
                    new RentInfo {Id = 1,},
                    new RentInfo {Id = 2},
                    new RentInfo {Id = 3}
                };
                var repo = new RentInfoRepository(context);

                //Act
                await repo.AddRangeAsync(payments);
                var expectedResult = 3;
                var actualResult = context.RentInfos.Local.Count();

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
                var payment = new RentInfo {Id = 1};
                var repo = new RentInfoRepository(context);

                //Act
                var expectedResult = 0;
                context.RentInfos.Add(payment);
                await repo.RemoveAsync(payment);
                var actualResult = context.RentInfos.Local.Count();

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
                var payments = new List<RentInfo>
                {
                    new RentInfo {Id = 1,},
                    new RentInfo {Id = 2},
                    new RentInfo {Id = 3}
                };
                var repo = new RentInfoRepository(context);

                //Act
                var expectedResult = 0;
                await context.RentInfos.AddRangeAsync(payments);
                await repo.RemoveRangeAsync(payments);
                var actualResult = context.RentInfos.Local.Count();

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
                var payment = new RentInfo {Id = 1};
                var repo = new RentInfoRepository(context);

                //Act
                var expectedResult = payment;
                context.RentInfos.Add(payment);
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
                var payments = new List<RentInfo>
                {
                    new RentInfo {Id = 1,},
                    new RentInfo {Id = 2},
                    new RentInfo {Id = 3}
                };
                var repo = new RentInfoRepository(context);

                //Act
                foreach (var payment in payments)
                {
                    context.RentInfos.Add(payment);
                }

                var actualResult = await repo.GetAllAsync();
                var expectedResult = context.RentInfos;

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
                var good = new RentInfo {Id = 1};
                var repo = new RentInfoRepository(context);

                //Act
                await repo.UpdateAsync(good);
                var expectedResult = EntityState.Modified;
                var actualResult = context.Entry(good).State;

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task FindAsyncTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
                var good = new RentInfo { Id = 1 };
                var repo = new RentInfoRepository(context);
                context.RentInfos.Add(good);
                context.SaveChanges();
                //Act
                var actualResult = (await repo.FindAsync(p => p.Id == good.Id)).FirstOrDefault();
                var expectedResult = good;


                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
    }
}