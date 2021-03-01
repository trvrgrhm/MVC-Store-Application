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
        /// <summary>
        /// Checks that a correct username and password combination returns an IUser object.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        [Theory]
        [InlineData("admin", "cLev3rPas$word")]
        [InlineData("customer", "cLev3rPas$word")]
        public void AttemptSignInWithUsernameAndPassword_Successful(string username, string password)
        {
            //Arrange
            var options = GetDbContextWithAdminAndCustomer();
            using (var context = new StoreDbContext(options))
            {
                var repo = new Repository(context);
                //Act
                var result = repo.AttemptSignInWithUsernameAndPassword(username, password);
                //Assert
                Assert.NotNull(result);
            }
        }
        /// <summary>
        /// Checks that an incorrect username and password combination returns null.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        [Theory]
        [InlineData("notAdmin", "cLev3rPas$word")]
        [InlineData("notCustomer", "cLev3rPas$word")]
        [InlineData("admin", "incorrectpassword")]
        [InlineData("customer", "incorrectpassword")]
        public void AttemptSignInWithUsernameAndPassword_Failure(string username, string password)
        {
            //Arrange
            var options = GetDbContextWithAdminAndCustomer();
            using (var context = new StoreDbContext(options))
            {
                var repo = new Repository(context);
                //Act
                var result = repo.AttemptSignInWithUsernameAndPassword(username, password);
                //Assert
                Assert.Null(result);
            }
        }

        /// <summary>
        /// Checks that the UsernameAlreadyExists method returns true if the username already exists and false if it doesn't
        /// </summary>
        /// <param name="username">username being checked</param>
        /// <param name="userExists">what the result of the method should be</param>
        [Theory]
        [InlineData("admin", true)]
        [InlineData("customer", true)]
        [InlineData("notadmin", false)]
        [InlineData("notcustomer", false)]
        public void UsernameAlreadyExists_CheckSuccessAndFailure(string username, bool userExists)
        {
            //Arrange
            var options = GetDbContextWithAdminAndCustomer();
            using (var context = new StoreDbContext(options))
            {
                var repo = new Repository(context);
                //Act
                var result = repo.UsernameAlreadyExists(username);
                //Assert
                Assert.Equal(userExists, result);
            }
        }

        /// <summary>
        /// Checks that AttemptAddCustomerToDb returns true when customers with unique usernames are passed to it.
        /// </summary>
        /// <param name="customer"></param>
        [Theory]
        [MemberData(nameof(CustomerDataUniqueNames))]
        public void AttemptAddCustomerToDb_Successful(Customer customer)
        {
            //Arrange
            var options = GetDbContextWithAdminAndCustomer();
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

        /// <summary>
        /// Checks that AttemptAddCustomerToDb returns false when customers passed in have a common username with other IUsers in the Db.
        /// </summary>
        /// <param name="customer"></param>
        [Theory]
        [MemberData(nameof(CustomerDataNamesTaken))]
        public void AttemptAddCustomerToDb_Failure(Customer customer)
        {
            //Arrange
            var options = GetDbContextWithAdminAndCustomer();
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
        /// Checks that UserIsCustomer returns true if the provided id matches a user in the database.
        /// </summary>
        [Fact]
        public void UserIsCustomer_Successful()
        {
            //Arrange
            var options = GetDbContextWithAdminAndCustomer();
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                //Act
                var result = repo.UserIsCustomer(testCustomer.UserId);
                //Assert
                Assert.True(result);
            }
        }
        /// <summary>
        /// Checks that UserIsCustomer returns false if the provided id does not match a user in the database.
        /// </summary>
        [Fact]
        public void UserIsCustomer_Failure()
        {
            //Arrange
            var options = GetDbContextWithAdminAndCustomer();
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                //Act
                var result = repo.UserIsCustomer(Guid.NewGuid());
                //Assert
                Assert.False(result);
            }
        }



        /// <summary>
        /// creates a DbContextOptions
        /// </summary>
        /// <returns></returns>
        public DbContextOptions<StoreDbContext> GetDbContextWithAdminAndCustomer(){
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "repoTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                var admin = new Administrator()
                {
                    UserId = new Guid("37277159-5c1b-4690-a774-912525e2e0be"),
                    Username = "admin",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    Acesslevel = AdminAccessLevel.EditAccess
                };
                context.Locations.Add(testCustomer.DefaultLocation);
                context.Administrators.Add(admin);
                context.Customers.Add(testCustomer);
                context.SaveChanges();
            }
            return options;
        }

        /// <summary>
        /// This is the customer that is stored in the in-memory database in GetDbContextWithAdminAndCustomer()
        /// </summary>
        Customer testCustomer = new Customer()
        {
            UserId = new Guid("fd7ebe9a-1081-4a28-b8a3-c90092a30bee"),
            Username = "customer",
            Password = "cLev3rPas$word",
            Fname = "First",
            LName = "Last",
            DefaultLocation = new Location()
            {
                LocationId = Guid.NewGuid(),
                Name = "Location 1"
            }
    };

        /// <summary>
        /// Contains new Customer objects that are not in the seeded db
        /// </summary>
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
        /// <summary>
        /// Contains new Customer objects that will have conflicts with the seeded db
        /// </summary>
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
