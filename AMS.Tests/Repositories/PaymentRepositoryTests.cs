using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace AMS.Tests.Repositories
{
    [TestFixture]
    class PaymentRepositoryTests
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
                var payment = new Payment { Id = 1};
                var repo = new PaymentRepository(context);

                //Act
                await repo.AddAsync(payment);
                var expectedResult = 1;
                var actualResult = context.Payments.Local.Count();

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
                var payments = new List<Payment>
                {
                    new Payment { Id = 1, },
                    new Payment { Id = 2 },
                    new Payment { Id = 3 }
                };
                var repo = new PaymentRepository(context);

                //Act
                await repo.AddRangeAsync(payments);
                var expectedResult = 3;
                var actualResult = context.Payments.Local.Count();

                //Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public async Task GetPaymentsByApartmentTest()
        {
            //Arrange
            using (var context = new ApplicationDbContext(options))
            {
               
                var apartment = new Apartment
                {
                    Id= 1,
                };
                var payments = new List<Payment>
                {
                    new Payment { Id = 1, ApartmentId = apartment.Id},
                    new Payment { Id = 2, ApartmentId = apartment.Id},
                    new Payment { Id = 3, ApartmentId = apartment.Id}
                };
                var repo = new PaymentRepository(context);

                //Act
                await repo.AddRangeAsync(payments);
                await repo.GetPaymentsByApartment(apartment.Id);
                var expectedResult = 3;
                var actualResult = context.Payments.Local.Count();

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
                var payment = new Payment { Id = 1 };
                var repo = new PaymentRepository(context);

                //Act
                var expectedResult = 0;
                context.Payments.Add(payment);
                await repo.RemoveAsync(payment);
                var actualResult = context.Payments.Local.Count();

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
                var payments = new List<Payment>
                {
                    new Payment { Id = 1, },
                    new Payment { Id = 2 },
                    new Payment { Id = 3 }
                };
                var repo = new PaymentRepository(context);

                //Act
                var expectedResult = 0;
                await context.Payments.AddRangeAsync(payments);
                await repo.RemoveRangeAsync(payments);
                var actualResult = context.Payments.Local.Count();

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
                var payment = new Payment { Id = 1 };
                var repo = new PaymentRepository(context);

                //Act
                var expectedResult = payment;
                context.Payments.Add(payment);
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
                var payments = new List<Payment>
                {
                    new Payment { Id = 1, },
                    new Payment { Id = 2 },
                    new Payment { Id = 3 }
                };
                var repo = new PaymentRepository(context);

                //Act
                foreach (var payment in payments)
                {
                    context.Payments.Add(payment);
                }
                var actualResult = await repo.GetAllAsync();
                var expectedResult = context.Payments;

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
                var good = new Payment { Id = 1};
                var repo = new PaymentRepository(context);

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
