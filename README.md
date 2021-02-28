# MVC Store Application
## Project Description
This is a ASP.NET Core MVC project utilizing Entity Framework Core to create a Web Store Application that allows a user to create an account then view orders by user and store location. The user can create an order, view their order history, and view the order history of a store location. It also allows administrators to view store location inventories and add products to inventories.
## Technologies Used
* ASP.NET MVC 5.2.7
* .NET 5
* Entity Framework 5.0.2 
## Features
* Customers can view store inventories.
* Customers can add multiple items to their carts.
* Customers can checkout and view past orders.
* Administrators can create store locations.
* Administrators can add products to store location inventories.
To-do list
* Update styling
## Getting Started
* Open a command prompt in the directory where you want to create the project.
* Use the command `git clone https://github.com/trvrgrhm/MVC-Store-Application.git` to download the project.
* Install Visual Studio to open the solution.
* Also install SQL server on your computer.
* Open MvcStoreApplication/MvcStoreApplicatoin.sln in visual studio.
* Update the connection string in the StoreDbContext class in the RepositoryLayer Project.
* Open Visual Studio's Package Manager Console (PMC). In the PMC, run `update-database` to add the migrations to the database in your connection string.
* After you have a database, you can run the development version of the project using IIS.
* If you click the Play button at the top of the window, Visual Studio will build and run the program.
## Usage
Before running the application, you need to create an administrator account.
After running the application, you can login with the administrator account to create store locations and add products to their inventories.
After store locations have inventories, you can sign out of the administrator account, and create a customer account.
When logged in as a customer, you can add items to your cart, checkout, and view past orders.
## License
This project uses the following license: [MIT License](https://github.com/trvrgrhm/MVC-Store-Application/blob/main/LICENSE)
