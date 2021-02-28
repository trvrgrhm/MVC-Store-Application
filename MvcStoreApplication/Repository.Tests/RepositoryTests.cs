using Microsoft.EntityFrameworkCore;
using Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using Xunit;

namespace RepositoryLayer.Tests
{
    public class RepositoryTests
    {
        [Theory]
        [InlineData("admin", "cLev3rPas$word")]
        [InlineData("customer", "cLev3rPas$word")]
        public void AttemptSignInWithUsernameAndPassword_Successful(string username, string password)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "repoTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var admin = new Administrator()
                {
                    UserId = Guid.NewGuid(),
                    Username = "admin",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    Acesslevel = AdminAccessLevel.EditAccess
                };
                var customer = new Customer()
                {
                    UserId = Guid.NewGuid(),
                    Username = "customer",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    DefaultLocation = location
                };
                context.Locations.Add(location);
                context.Administrators.Add(admin);
                context.Customers.Add(customer);
                context.SaveChanges();
                //Act
                var result = repo.AttemptSignInWithUsernameAndPassword(username, password);
                //Assert
                Assert.NotNull(result);
            }
        }
        [Theory]
        [InlineData("notAdmin", "cLev3rPas$word")]
        [InlineData("notCustomer", "cLev3rPas$word")]
        [InlineData("admin", "incorrectpassword")]
        [InlineData("customer", "incorrectpassword")]
        public void AttemptSignInWithUsernameAndPassword_Failure(string username, string password)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "repoTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var admin = new Administrator()
                {
                    UserId = Guid.NewGuid(),
                    Username = "admin",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    Acesslevel = AdminAccessLevel.EditAccess
                };
                var customer = new Customer()
                {
                    UserId = Guid.NewGuid(),
                    Username = "customer",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    DefaultLocation = location
                };
                context.Locations.Add(location);
                context.Administrators.Add(admin);
                context.Customers.Add(customer);
                context.SaveChanges();
                //Act
                var result = repo.AttemptSignInWithUsernameAndPassword(username, password);
                //Assert
                Assert.Null(result);
            }
        }
        [Theory]
        [InlineData("admin", true)]
        [InlineData("customer", true)]
        [InlineData("notadmin", false)]
        [InlineData("notcustomer", false)]
        public void UsernameAlreadyExists_CheckSuccessAndFailure(string username, bool userExists)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "repoTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var admin = new Administrator()
                {
                    UserId = Guid.NewGuid(),
                    Username = "admin",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    Acesslevel = AdminAccessLevel.EditAccess
                };
                var customer = new Customer()
                {
                    UserId = Guid.NewGuid(),
                    Username = "customer",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    DefaultLocation = location
                };
                context.Locations.Add(location);
                context.Administrators.Add(admin);
                context.Customers.Add(customer);
                context.SaveChanges();
                //Act
                var result = repo.UsernameAlreadyExists(username);
                //Assert
                Assert.Equal(result, userExists);
            }
        }

        [Theory]
        [MemberData(
            nameof(CustomerDataUniqueNames)
        )]
        public void AttemptAddCustomerToDb_Successful(Customer customer)
        {
            //Arrange
            var options = SetUpDbWithAdminAndCustomer();
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                //Act
                var result = repo.AttemptAddCustomerToDb(customer);
                //Assert
                Assert.True(result);
            }
        }
        [Theory]
        [MemberData(
            nameof(CustomerDataNamesTaken)
        )]
        public void AttemptAddCustomerToDb_Failure(Customer customer)
        {
            //Arrange
            var options = SetUpDbWithAdminAndCustomer();
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                //Act
                var result = repo.AttemptAddCustomerToDb(customer);
                //Assert
                Assert.False(result);
            }
        }

        /// <summary>
        /// creates a DbContextOptions
        /// </summary>
        /// <returns></returns>
        public DbContextOptions<StoreDbContext> SetUpDbWithAdminAndCustomer(){
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "repoTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var admin = new Administrator()
                {
                    UserId = Guid.NewGuid(),
                    Username = "admin",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    Acesslevel = AdminAccessLevel.EditAccess
                };
                var dbCustomer = new Customer()
                {
                    UserId = Guid.NewGuid(),
                    Username = "customer",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    DefaultLocation = location
                };
                context.Locations.Add(location);
                context.Administrators.Add(admin);
                context.Customers.Add(dbCustomer);
                context.SaveChanges();
            }
            return options;
        }


        public static IEnumerable<object[]> CustomerDataUniqueNames
        {
            get
            {
                // Or this could read from a file. :)
                return new []
                {
                new object[] {
                    new Customer()
                    {
                        UserId = Guid.NewGuid(),
                        Username = "customer1",
                        Password = "cLev3rPas$word",
                        Fname = "First",
                        LName = "Last",
                        DefaultLocation = new Location{LocationId = Guid.NewGuid(), Name = "Location" }
                    }
                },
                new object[] {
                    new Customer()
                    {
                        UserId = Guid.NewGuid(),
                        Username = "customer2",
                        Password = "cLev3rPas$word",
                        Fname = "First",
                        LName = "Last",
                        DefaultLocation = new Location{LocationId = Guid.NewGuid(), Name = "Location" }
                    }
                },
            };
            }
        }
        public static IEnumerable<object[]> CustomerDataNamesTaken
        {
            get
            {
                // Or this could read from a file. :)
                return new[]
                {
                new object[] {
                    new Customer()
                    {
                        UserId = Guid.NewGuid(),
                        Username = "customer",
                        Password = "cLev3rPas$word",
                        Fname = "First",
                        LName = "Last",
                        DefaultLocation = new Location{LocationId = Guid.NewGuid(), Name = "Location" }
                    }
                },
                new object[] {
                    new Customer()
                    {
                        UserId = Guid.NewGuid(),
                        Username = "admin",
                        Password = "cLev3rPas$word",
                        Fname = "First",
                        LName = "Last",
                        DefaultLocation = new Location{LocationId = Guid.NewGuid(), Name = "Location" }
                    }
                },
            };
            }
        }
    }
}
