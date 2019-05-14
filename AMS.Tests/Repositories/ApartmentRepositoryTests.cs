using AMS.Core.Models;
using AMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Tests.Repositories
{
    public class ApartmentRepositoryTests
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
                    var apart = new Apartment {Id = 1};
                    var repo = new ApartmentRepository(context);

                    //Act
                    await repo.AddAsync(apart);
                    var expectedResult = 1;
                    var actualResult = context.Apartments.Local.Count();

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
                    var apartments = new List<Apartment>
                    {
                        new Apartment {Id = 1,},
                        new Apartment {Id = 2},
                        new Apartment {Id = 3}
                    };
                    var repo = new ApartmentRepository(context);

                    //Act
                    await repo.AddRangeAsync(apartments);
                    var expectedResult = 3;
                    var actualResult = context.Apartments.Local.Count();

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
                    var apart = new Apartment {Id = 1};
                    var repo = new ApartmentRepository(context);

                    //Act
                    var expectedResult = 0;
                    context.Apartments.Add(apart);
                    await repo.RemoveAsync(apart);
                    var actualResult = context.Apartments.Local.Count();

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
                    var apart = new List<Apartment>
                    {
                        new Apartment {Id = 1,},
                        new Apartment {Id = 2},
                        new Apartment {Id = 3}
                    };
                    var repo = new ApartmentRepository(context);

                    //Act
                    var expectedResult = 0;
                    await context.Apartments.AddRangeAsync(apart);
                    await repo.RemoveRangeAsync(apart);
                    var actualResult = context.Apartments.Local.Count();

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
                    var apart = new Apartment {Id = 1};
                    var repo = new ApartmentRepository(context);

                    //Act
                    var expectedResult = apart;
                    context.Apartments.Add(apart);
                    var result = await repo.GetAsync(apart.Id);

                    //Assert
                    Assert.AreEqual(expectedResult, result);
                }
            }

            [Test]
            public void GetWithUsersTest()
            {
                //Arrange
                using (var context = new ApplicationDbContext(options))
                {
                    var apart = new Apartment { Id = 1 };
                    var inhabitants = new List<User>
                    {
                        new User{ Id = "1",ApartmentId = 1,Apartment = apart},
                        new User{ Id = "2",ApartmentId = 1,Apartment = apart},
                    };
                    var repo = new ApartmentRepository(context);
                    apart.Inhabitants = inhabitants;
                    //Act
                    var expectedResult = apart;
                    context.Apartments.Add(apart);
                    context.SaveChanges();
                    var result = repo.GetWithUsers(apart.Id);

                    //Assert
                    Assert.AreEqual(apart, result);
                }
            }

            [Test]
            public void GetAllWithUsersTest()
            {
                //Arrange
                using (var context = new ApplicationDbContext(options))
                {
                    var apart = new Apartment { Id = 1 };
                    var inhabitants = new List<User>
                    {
                        new User{ Id = "1",ApartmentId = 1,Apartment = apart},
                        new User{ Id = "2",ApartmentId = 1,Apartment = apart},
                    };
                    var repo = new ApartmentRepository(context);
                    apart.Inhabitants = inhabitants;
                    //Act
                    var expectedResult = apart;
                    context.Apartments.Add(apart);
                    context.SaveChanges();
                    var result = repo.GetAllWithUsers();

                    //Assert
                    Assert.AreEqual(result.Count(),1);
                }
            }

            [Test]
            public async Task GetAllAsyncTest()
            {
                //Arrange
                using (var context = new ApplicationDbContext(options))
                {
                    var apart = new List<Apartment>
                    {
                        new Apartment {Id = 1,},
                        new Apartment {Id = 2},
                        new Apartment {Id = 3}
                    };
                    var repo = new ApartmentRepository(context);

                    //Act
                    foreach (var apartm in apart)
                    {
                        context.Apartments.Add(apartm);
                    }

                    var actualResult = await repo.GetAllAsync();
                    var expectedResult = context.Apartments;

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
                    var good = new Apartment {Id = 1};
                    var repo = new ApartmentRepository(context);

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
}
