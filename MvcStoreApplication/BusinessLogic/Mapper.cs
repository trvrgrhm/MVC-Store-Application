using Models;
using Models.ViewModels;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Mapper
    {
        private readonly Repository _repository;

        public Mapper(Repository repository)
        {
            _repository = repository;
        }

        //admin
        /// <summary>
        /// Converts an Administrator to a viewmodel
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public AdministratorViewModel ConvertAdministratorToAdministratorViewModel(Administrator admin)
        {
            AdministratorViewModel adminViewModel = new AdministratorViewModel()
            {
                UserId = admin.UserId,
                Username = admin.Username,
                Password = admin.Password,
                Fname = admin.Fname,
                LName = admin.LName,
                Acesslevel = admin.Acesslevel
            };
            return adminViewModel;
        }
        /// <summary>
        /// Converts an administrator viewmodel into an Administrator
        /// </summary>
        /// <param name="administratorViewModel"></param>
        /// <returns></returns>
        public Administrator ConvertAdministratorViewModelToAdministrator(AdministratorViewModel administratorViewModel)
        {
            Administrator admin = new Administrator()
            {
                UserId = administratorViewModel.UserId,
                Username = administratorViewModel.Username,
                Password = administratorViewModel.Password,
                Fname = administratorViewModel.Fname,
                LName = administratorViewModel.LName,
                Acesslevel = administratorViewModel.Acesslevel
            };
            return admin;
        }
        
        //customer
        /// <summary>
        /// Converts a customer model to a customer viewmodel
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public CustomerViewModel ConvertCustomerToCustomerViewModel(Customer customer)
        {
            if (customer.DefaultLocation == null)
            {
                customer.DefaultLocation = _repository.GetDefautLocation();
            }
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                UserId = customer.UserId,
                Username = customer.Username,
                Password = customer.Password,
                Fname = customer.Fname,
                LName = customer.LName,
                DefaultLocationId = customer.DefaultLocation.LocationId,
                DefaultStoreName = customer.DefaultLocation.Name
            };
            return customerViewModel;
        }
        /// <summary>
        /// Converts a customer veiwmodel into a customer model
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        public Customer ConvertCustomerViewModelToCustomer(CustomerViewModel customerViewModel)
        {
            Customer customer = new Customer()
            {
                UserId = customerViewModel.UserId,
                Username = customerViewModel.Username,
                Password = customerViewModel.Password,
                Fname = customerViewModel.Fname,
                LName = customerViewModel.LName,
                DefaultLocation =  _repository.GetLocationById(customerViewModel.DefaultLocationId) 
            };
            return customer;
        }

        //inventory
        /// <summary>
        /// Converts inventory model to inventory viewmodel
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public InventoryViewModel ConvertInventoryToInventoryViewModel(Inventory inventory)
        {
            InventoryViewModel inventoryViewModel = new InventoryViewModel()
            {
                InventoryId = inventory.InventoryId,
                Quantity = inventory.Quantity,
                //foreign keys
                LocationId = inventory.Location.LocationId,
                ProductId = inventory.Product.ProductId,
                //product stuff
                ProductName = inventory.Product.ProductName,
                Price = inventory.Product.Price,
                Description = inventory.Product.Description,
                //location stuff
                LocationName = inventory.Location.Name         
            };
            return inventoryViewModel;
        }
        /// <summary>
        /// Converts an inventory viewmodel to an inventory model
        /// </summary>
        /// <param name="inventoryViewModel"></param>
        /// <returns></returns>
        public Inventory ConvertInventoryViewModelToInventory(InventoryViewModel inventoryViewModel)
        {
            Inventory inventory = new Inventory()
            {
                InventoryId = inventoryViewModel.InventoryId,
                Quantity = inventoryViewModel.Quantity,
                //foreign keys
                Location = _repository.GetLocationById(inventoryViewModel.LocationId),
                Product = _repository.GetProductById(inventoryViewModel.ProductId),

            };
            return inventory;
        }

        //product
        /// <summary>
        /// Converts a product viewmodel into a model and returns it
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        public Product ConvertProductViewModelToProduct(ProductViewModel productViewModel)
        {
            Product product = new Product()
            {
                ProductId = productViewModel.ProductId,
                ProductName = productViewModel.ProductName,
                Price = productViewModel.Price,
                Description = productViewModel.Description
            };
            return product;
        }
        /// <summary>
        /// Converts a product into a product viewmodel and returns it
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ProductViewModel ConvertProductToProductViewModel(Product product)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description
            };
            return productViewModel;
        }

        //location
        /// <summary>
        /// Converts location model to location viewmodel
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public LocationViewModel ConvertLocationToLocationViewModel(Location location)
        {
            LocationViewModel locationViewModel = new LocationViewModel()
            {
                LocationId = location.LocationId,
                Name = location.Name

            };
            return locationViewModel;
        }
        /// <summary>
        /// Converts location viewmodel to Location
        /// </summary>
        /// <param name="locationViewModel"></param>
        /// <returns></returns>
        public Location ConvertLocationViewModelToLocation(LocationViewModel locationViewModel)
        {
            Location location = new Location()
            {
                LocationId = locationViewModel.LocationId,
                Name = locationViewModel.Name

            };
            return location;
        }

        //orderline
        /// <summary>
        /// Converts orderline viewmodel to OrderLine
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public OrderLine ConvertOrderLineViewModelToOrderLine(OrderLineViewModel viewModel)
        {
            if(viewModel == null)
            {
                return null;
            }
            OrderLine line = new OrderLine()
            {
                Quantity = viewModel.Quantity,
                OrderLineId = viewModel.OrderLineId
            };
            Inventory inv = _repository.GetInventoryById(viewModel.InventoryId);
            Order ord = _repository.GetOrderById(viewModel.OrderId);
            if (inv != null)
            {
                line.Inventory = inv;
            }
            if(ord != null)
            {
                line.Order = ord;
            }
            return line;
        }
        /// <summary>
        /// Converts OrderLine to orderline viewmodel.
        /// </summary>
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public OrderLineViewModel ConvertOrderLineToOrderLineViewModel(OrderLine orderLine)
        {

            var viewModel = new OrderLineViewModel()
            {
                OrderId = orderLine.Order.OrderId,
                OrderLineId = orderLine.OrderLineId,
                Quantity = orderLine.Quantity,
            };
            var inventory = _repository.GetInventoryById(orderLine.Inventory.InventoryId);
            if (inventory != null)
            {
                
                viewModel.InventoryId = orderLine.Inventory.InventoryId;

                var product = inventory.Product;
                var loc = inventory.Location;
                if (product != null)
                {
                    viewModel.Price = product.Price;
                    viewModel.ProductName = product.ProductName;
                    viewModel.TotalPrice = product.Price * orderLine.Quantity;
                }
                if (loc != null)
                {
                    viewModel.StoreName = inventory.Location.Name;
                }

            }
            return viewModel;

        }
    }

}
