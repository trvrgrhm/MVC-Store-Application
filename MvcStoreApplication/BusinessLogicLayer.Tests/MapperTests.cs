using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using RepositoryLayer;
using System;
using Xunit;

namespace BusinessLogicLayer.Tests
{
    public class MapperTests
    {
        //admin
        [Fact]
        public void ConvertAdministratorToAdministratorViewModel_ChecksAllValues()
        {
            //Arrange
            //this method doesn't require a repo
            Mapper mapper = new Mapper(null);
            var admin = new Administrator()
            {
                UserId = Guid.NewGuid(),
                Username = "coolUsername",
                Password = "cLev3rPas$word",
                Fname = "First",
                LName = "Last",
                Acesslevel = AdminAccessLevel.EditAccess
            };
            //Act
            var result = mapper.ConvertAdministratorToAdministratorViewModel(admin);
            //Assert
            Assert.Equal(admin.UserId, result.UserId);
            Assert.Equal(admin.Username, result.Username);
            Assert.Equal(admin.Password, result.Password);
            Assert.Equal(admin.Fname, result.Fname);
            Assert.Equal(admin.LName, result.LName);
            Assert.Equal(admin.Acesslevel, result.Acesslevel);
        }
        [Fact]
        public void ConvertAdministratorViewModelToAdministrator_ChecksAllValues()
        {
            //Arrange
            //this method doesn't require a repo
            Mapper mapper = new Mapper(null);
            var adminViewModel = new AdministratorViewModel()
            {
                UserId = Guid.NewGuid(),
                Username = "coolUsername",
                Password = "cLev3rPas$word",
                Fname = "First",
                LName = "Last",
                Acesslevel = AdminAccessLevel.EditAccess
            };
            //Act
            var result = mapper.ConvertAdministratorViewModelToAdministrator(adminViewModel);
            //Assert
            Assert.Equal(adminViewModel.UserId, result.UserId);
            Assert.Equal(adminViewModel.Username, result.Username);
            Assert.Equal(adminViewModel.Password, result.Password);
            Assert.Equal(adminViewModel.Fname, result.Fname);
            Assert.Equal(adminViewModel.LName, result.LName);
            Assert.Equal(adminViewModel.Acesslevel, result.Acesslevel);
        }
        //customer 
        [Fact]
        public void ConvertCustomerToCustomerViewModel_ChecksAllValues()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "mapperTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                Mapper mapper = new Mapper(repo);
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var cust = new Customer()
                {
                    UserId = Guid.NewGuid(),
                    Username = "coolUsername",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    DefaultLocation = location
                };
                //Act
                context.Locations.Add(location);
                context.SaveChanges();
                var result = mapper.ConvertCustomerToCustomerViewModel(cust);
                //Assert
                Assert.Equal(cust.UserId, result.UserId);
                Assert.Equal(cust.Username, result.Username);
                Assert.Equal(cust.Password, result.Password);
                Assert.Equal(cust.Fname, result.Fname);
                Assert.Equal(cust.LName, result.LName);
                Assert.Equal(cust.DefaultLocation.LocationId, result.DefaultLocationId);
            }
        }
        [Fact]
        public void ConvertCustomerViewModelToCustomer_ChecksAllValues()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "mapperTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                Mapper mapper = new Mapper(repo);
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var customerViewModel = new CustomerViewModel()
                {
                    UserId = Guid.NewGuid(),
                    Username = "coolUsername",
                    Password = "cLev3rPas$word",
                    Fname = "First",
                    LName = "Last",
                    DefaultLocationId = location.LocationId
                };
                context.Locations.Add(location);
                context.SaveChanges();
                //Act
                var result = mapper.ConvertCustomerViewModelToCustomer(customerViewModel);
                //Assert
                Assert.Equal(customerViewModel.UserId, result.UserId);
                Assert.Equal(customerViewModel.Username, result.Username);
                Assert.Equal(customerViewModel.Password, result.Password);
                Assert.Equal(customerViewModel.Fname, result.Fname);
                Assert.Equal(customerViewModel.LName, result.LName);
                Assert.Equal(customerViewModel.DefaultLocationId, result.DefaultLocation.LocationId);
            }
        }
        //inventory
        [Fact]
        public void ConvertInventoryToInventoryViewModel_ChecksAllValues()
        {
            //Arrange
            //this method doesn't require a repo
            Mapper mapper = new Mapper(null);
            var product = new Product()
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Product 1",
                Price = 1.00,
                Description = "Descriptive description."
            };
            var location = new Location()
            {
                LocationId = Guid.NewGuid(),
                Name = "Location 1"
            };
            var inv = new Inventory()
            {
                InventoryId = Guid.NewGuid(),
                Location = location,
                Product = product,
                Quantity = 2
            };
            //Act
            var result = mapper.ConvertInventoryToInventoryViewModel(inv);
            //Assert
            Assert.Equal(inv.InventoryId, result.InventoryId);
            Assert.Equal(inv.Location.LocationId, result.LocationId);
            Assert.Equal(inv.Location.Name, result.LocationName);
            Assert.Equal(inv.Product.ProductId, result.ProductId);
            Assert.Equal(inv.Product.ProductName, result.ProductName);
            Assert.Equal(inv.Product.Price, result.Price);
            Assert.Equal(inv.Product.Description, result.Description);
            Assert.Equal(inv.Quantity, result.Quantity);
        }
        [Fact]
        public void ConvertInventoryViewModelToInventory_ChecksAllValues()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "mapperTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                Mapper mapper = new Mapper(repo);
                var product = new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product 1",
                    Price = 1.00,
                    Description = "Descriptive description."
                };
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var inventoryViewModel = new InventoryViewModel()
                {
                    InventoryId = Guid.NewGuid(),
                    LocationId = location.LocationId,
                    LocationName = location.Name,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.Description,
                    Quantity = 2

                };
                context.Products.Add(product);
                context.Locations.Add(location);
                context.SaveChanges();
                //Act
                var result = mapper.ConvertInventoryViewModelToInventory(inventoryViewModel);
                //Assert
                Assert.Equal(inventoryViewModel.InventoryId, result.InventoryId);
                Assert.Equal(inventoryViewModel.LocationId, result.Location.LocationId);
                Assert.Equal(inventoryViewModel.LocationName, result.Location.Name);
                Assert.Equal(inventoryViewModel.ProductId, result.Product.ProductId);
                Assert.Equal(inventoryViewModel.ProductName, result.Product.ProductName);
                Assert.Equal(inventoryViewModel.Price, result.Product.Price);
                Assert.Equal(inventoryViewModel.Description, result.Product.Description);
                Assert.Equal(inventoryViewModel.Quantity, result.Quantity);
            }
        }
        //product
        [Fact]
        public void ConvertProductToProductViewModel_ChecksAllValues()
        {
            //Arrange
            //this method doesn't require a repo
            Mapper mapper = new Mapper(null);
            var product = new Product()
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Product 1",
                Price = 1.00,
                Description = "Descriptive description."
            };
            //Act
            var result = mapper.ConvertProductToProductViewModel(product);
            //Assert
            Assert.Equal(product.ProductId, result.ProductId);
            Assert.Equal(product.ProductName, result.ProductName);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Description, result.Description);
        }
        [Fact]
        public void ConvertProductViewModelToProduct_ChecksAllValues()
        {
            //Arrange
            //this method doesn't require a repo
            Mapper mapper = new Mapper(null);
            var productViewModel = new ProductViewModel()
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Product 1",
                Price = 1.00,
                Description = "Descriptive description."
            };
            //Act
            var result = mapper.ConvertProductViewModelToProduct(productViewModel);
            //Assert
            Assert.Equal(productViewModel.ProductId, result.ProductId);
            Assert.Equal(productViewModel.ProductName, result.ProductName);
            Assert.Equal(productViewModel.Price, result.Price);
            Assert.Equal(productViewModel.Description, result.Description);
        }
        //location
        [Fact]
        public void ConvertLocationToLocationViewModel_ChecksAllValues()
        {
            //Arrange
            //this method doesn't require a repo
            Mapper mapper = new Mapper(null);
            var location = new Location()
            {
                LocationId = Guid.NewGuid(),
                Name = "Location 1",
            };
            //Act
            var result = mapper.ConvertLocationToLocationViewModel(location);
            //Assert
            Assert.Equal(location.LocationId, result.LocationId);
            Assert.Equal(location.Name, result.Name);
        }
        [Fact]
        public void ConvertLocationViewModelToLocation_ChecksAllValues()
        {
            //Arrange
            //this method doesn't require a repo
            Mapper mapper = new Mapper(null);
            var locationViewModel = new LocationViewModel()
            {
                LocationId = Guid.NewGuid(),
                Name = "Location 1",
            };
            //Act
            var result = mapper.ConvertLocationViewModelToLocation(locationViewModel);
            //Assert
            Assert.Equal(locationViewModel.LocationId, result.LocationId);
            Assert.Equal(locationViewModel.Name, result.Name);
        }
        //orderline
        [Fact]
        public void ConvertOrderLineViewModelToOrderLine_ChecksObjectIds()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "mapperTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                Mapper mapper = new Mapper(repo);
                var product = new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product 1",
                    Price = 1.00,
                    Description = "Descriptive description."
                };
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var inv = new Inventory()
                {
                    InventoryId = Guid.NewGuid(),
                    Location = location,
                    Product = product,
                    Quantity = 2
                };
                var order = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Customer = null,
                    OrderIsComplete = true
                };
                var orderLineViewModel = new OrderLineViewModel()
                {
                    OrderLineId = Guid.NewGuid(),
                    InventoryId = inv.InventoryId,
                    OrderId = order.OrderId,
                    Quantity = 1
                };
                context.Products.Add(product);
                context.Locations.Add(location);
                context.Inventories.Add(inv);
                context.Orders.Add(order);
                context.SaveChanges();
                //Act
                var result = mapper.ConvertOrderLineViewModelToOrderLine(orderLineViewModel);
                //Assert
                Assert.Equal(orderLineViewModel.OrderLineId, result.OrderLineId);
                Assert.Equal(orderLineViewModel.OrderId, result.Order.OrderId);
                Assert.Equal(orderLineViewModel.InventoryId, result.Inventory.InventoryId);
            }
        }
        [Fact]
        public void ConvertOrderLineToOrderLineViewModel_ChecksAllValues()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "mapperTestDb")
            .Options;
            using (var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repo = new Repository(context);
                Mapper mapper = new Mapper(repo);
                var product = new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Product 1",
                    Price = 1.00,
                    Description = "Descriptive description."
                };
                var location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    Name = "Location 1"
                };
                var inv = new Inventory()
                {
                    InventoryId = Guid.NewGuid(),
                    Location = location,
                    Product = product,
                    Quantity = 3
                };
                var order = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Customer = null,
                    OrderIsComplete = true
                };
                var orderLine = new OrderLine()
                {
                    OrderLineId = Guid.NewGuid(),
                    Inventory = inv,
                    Order = order,
                    Quantity = 2
                };
                context.Products.Add(product);
                context.Locations.Add(location);
                context.Inventories.Add(inv);
                context.Orders.Add(order);
                context.SaveChanges();
                //Act
                var result = mapper.ConvertOrderLineToOrderLineViewModel(orderLine);
                //Assert
                Assert.Equal(orderLine.OrderLineId, result.OrderLineId);
                Assert.Equal(orderLine.Order.OrderId, result.OrderId);
                Assert.Equal(orderLine.Inventory.InventoryId, result.InventoryId);
                Assert.Equal(orderLine.Inventory.Location.Name, result.StoreName);
                Assert.Equal(orderLine.Inventory.Product.ProductName, result.ProductName);
                Assert.Equal(orderLine.Inventory.Product.Price, result.Price);
                Assert.Equal(orderLine.Quantity, result.Quantity);
                Assert.Equal(orderLine.Quantity*orderLine.Inventory.Product.Price, result.TotalPrice);
            }
        }
    }
}
