using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameStore.DataAccess.Entities;
using GameStore.DataAccess.Repositories;
using GameStore.Library.Interfaces;
using GameStore.Library.Model;
using Microsoft.EntityFrameworkCore;

namespace GameStore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var dependencies = new Dependencies();
            IGameStoreRepository gameStoreRepository = dependencies.CreateGameStoreRepository();
            RunUi(gameStoreRepository);
        }

        private static void RunUi(IGameStoreRepository gameStoreRepository)
        {
            Console.WriteLine("Welcome to GameStore");

            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("sc <customer name>:\tSearch a customer by name.");
                Console.WriteLine("ll:\tList all the store locations.");
                Console.WriteLine("o:\tPlace a new order.");
                Console.WriteLine("nc:\tCreate a new customer.");
                Console.WriteLine();
                Console.Write("Enter valid menu option, or \"q\" to quit: ");
                var input = Console.ReadLine();




                if (input.StartsWith("sc "))
                {
                    var name = input.Split(' ', 2)[1];
                    var customerList = gameStoreRepository.SearchCustomerName(name).ToList();

                    // if no customer with that name
                    Console.WriteLine();
                    if (customerList.Count == 0)
                    {
                        Console.WriteLine("No customer with that name");
                        Console.WriteLine();
                    }
                    // print all customers with that name
                    else
                    {
                        while (true)
                        {
                            PrintCustomerList(customerList);

                            Console.Write("Pick a customer number to show their order history, or \"m\" to go to main menu: ");
                            input = Console.ReadLine();

                            // valid customer #, print their history
                            if (int.TryParse(input, out var customerNum)
                                    && customerNum >= 1 && customerNum <= customerList.Count)
                            {
                                Library.Model.Customer customer = customerList[customerNum-1];
                                var customerOrderHistory = gameStoreRepository.GetCustomerOrderHistory(customer).ToList();
                                foreach (var order in customerOrderHistory)
                                {
                                    order.BuildShoppingCart();
                                }

                                Console.WriteLine();
                                Console.WriteLine($"This is the order history of {customer.GetFullName()}:");
                                Console.WriteLine();

                                for (int i = 1; i <= customerOrderHistory.Count(); i++)
                                {
                                    Console.WriteLine($"Order {i}\t\tLocationID: {customerOrderHistory[i-1].LocationId}\t" +
                                        $"Time: {customerOrderHistory[i-1].TimePlaced}");
                                    Console.WriteLine();
                                    Console.WriteLine("Product:\tQuantity:");
                                    foreach (KeyValuePair<Library.Model.Product, int> product in customerOrderHistory[i-1].ShoppingCart)
                                    {
                                        Console.WriteLine($"{product.Key.Name}\t\t{product.Value}");
                                    }
                                    Console.WriteLine();
                                }
                                break;
                            }
                            // go back to main menu
                            else if (input == "m")
                            {
                                Console.WriteLine();
                                break;
                            }
                            // invalid input
                            else
                            {
                                PrintInvalid(input);
                            }
                        }
                    }
                }








                // list all the locations. QUESTION: WHAT IF THERE IS NO LOCATION?
                else if (input == "ll")
                {
                    Console.WriteLine();

                    var allLocations = gameStoreRepository.GetAllLocations().ToList();

                    while (true)
                    {

                        PrintLocationList(allLocations);

                        Console.Write("Pick a location number to show its order history, or \"m\" to go to main menu: ");
                        input = Console.ReadLine();
                        // valid location number
                        if (int.TryParse(input, out var locationNum)
                                        && locationNum >= 1 && locationNum <= allLocations.Count)
                        {
                            Library.Model.Location location = allLocations[locationNum-1];
                            var locationOrderHistory = gameStoreRepository.GetLocationOrderHistory(location).ToList();

                            if (locationOrderHistory.Count == 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("This location has no order history.");
                                Console.WriteLine();
                                break;
                            }

                            foreach (var order in locationOrderHistory)
                            {
                                order.BuildShoppingCart();
                            }


                            Console.WriteLine();
                            Console.WriteLine($"This is the order history of store located in {location.City}, {location.State}:");
                            Console.WriteLine();

                            for (int i = 1; i <= locationOrderHistory.Count(); i++)
                            {
                                Console.WriteLine($"Order {i}\t\tCustomerId: {locationOrderHistory[i-1].CustomerId}\t\t" +
                                    $"Time: {locationOrderHistory[i-1].TimePlaced}");
                                Console.WriteLine();
                                Console.WriteLine("Product:\t\tQuantity:");
                                foreach (KeyValuePair<Library.Model.Product, int> item in locationOrderHistory[i-1].ShoppingCart)
                                {
                                    Console.WriteLine($"{item.Key.Name}\t\t{item.Value}");
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                        // back to main menu
                        else if (input == "m")
                        {
                            Console.WriteLine();
                            break;
                        }
                        // invalid input
                        else
                        {
                            PrintInvalid(input);
                        }
                    }
                }








                // NEED TO IMPLEMENT
                else if (input == "o")
                {
                    //ask for which customer (store name in string and search customer)
                    Console.WriteLine();
                    Console.Write("Which customer would you like to make the order for? (Enter customer name): ");
                    var customerName = Console.ReadLine();

                    var customerList = gameStoreRepository.SearchCustomerName(customerName).ToList();

                    // if no customer with that name
                    Console.WriteLine();
                    if (customerList.Count == 0)
                    {
                        Console.WriteLine("No customer with that name");
                        Console.WriteLine();
                    }
                    else
                    {
                        while (true)
                        {
                            PrintCustomerList(customerList);

                            Console.Write("Choose which customer, or \"m\" to go to main menu: ");
                            input = Console.ReadLine();

                            // valid customer #, list all locations and ask for location
                            if (int.TryParse(input, out var customerNum)
                                    && customerNum >= 1 && customerNum <= customerList.Count)
                            {
                                Library.Model.Customer orderCustomer = customerList[customerNum - 1];

                                var allLocations = gameStoreRepository.GetAllLocations().ToList();

                                while (true)
                                {
                                    Console.WriteLine();
                                    PrintLocationList(allLocations);

                                    Console.WriteLine();
                                    Console.Write("Choose which location the customer is placing the order for, or \"m\" to go to main menu: ");
                                    input = Console.ReadLine();

                                    // valid location number. List all the items in inventory
                                    if (int.TryParse(input, out var locationNum)
                                                    && locationNum >= 1 && locationNum <= allLocations.Count)
                                    {
                                        Library.Model.Location orderLocation = allLocations[locationNum - 1];
                                        orderLocation.BuildInventory();

                                        Library.Model.Order newOrder = new();
                                        newOrder.CustomerId = orderCustomer.Id;
                                        newOrder.LocationId = orderLocation.Id;

                                        Console.WriteLine();
                                        Console.WriteLine($"The inventory of location with Id {orderLocation.Id} at {orderLocation.City}, {orderLocation.State} is:");
                                        Console.WriteLine("(#, Product, Quantity)");
                                        Console.WriteLine();
                                        int counter = 1;
                                        foreach (var item in orderLocation.Inventory)
                                        {
                                            Console.WriteLine($"{counter}, {item.Key.Name}, {item.Value}");
                                            counter++;
                                        }

                                        //keep showing inventory and asking to add or place order
                                        while (true)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Your shopping cart contains the following items (Product, Quantity): ");
                                            foreach (var item in newOrder.ShoppingCart)
                                            {
                                                Console.WriteLine($"{item.Key.Name}, {item.Value}");
                                            }

                                            Console.WriteLine();
                                            Console.WriteLine("Choose what you would like to do");
                                            Console.WriteLine("add <Product name> <quantity>: add an inventory item with quantity to your shopping cart");
                                            Console.WriteLine("disc: discard the shopping cart (start from scratch)");
                                            Console.WriteLine("po: place the order");
                                            Console.WriteLine("m: go back to the menu");
                                            Console.WriteLine();
                                            Console.Write("Enter valid menu option: ");
                                            input = Console.ReadLine();

                                            if (input.StartsWith("add "))
                                            {
                                                string[] inputSplit = input.Split(' ');
                                                var prodName = "";
                                                for (int k=1; k<inputSplit.Length-1; k++)
                                                {
                                                    if (prodName == "")
                                                    {
                                                        prodName += inputSplit[k];
                                                    }
                                                    else
                                                    {
                                                        prodName += " " + inputSplit[k];
                                                    }
                                                }
                                                var qnt = inputSplit[inputSplit.Length - 1];
                                                //valid inventory number and quantity is int
                                                if (IsValidProductName(prodName, orderLocation.Inventory)
                                                    && int.TryParse(qnt, out var inventoryQnt))
                                                {
                                                    try
                                                    {
                                                        Library.Model.Product prodToAdd = new();
                                                        foreach (var item in orderLocation.Inventory)
                                                        {
                                                            if (item.Key.Name == prodName)
                                                            {
                                                                prodToAdd = item.Key;
                                                            }
                                                        }
                                                        newOrder.AddProduct(prodToAdd, inventoryQnt);
                                                        Console.WriteLine();
                                                        Console.WriteLine("Product added.");
                                                    }
                                                    catch (ArgumentException ae)
                                                    {
                                                        Console.WriteLine();
                                                        Console.WriteLine(ae.Message);
                                                        Console.WriteLine();
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine($"Not a valid product name: {prodName}");
                                                    Console.WriteLine();
                                                }
                                            }
                                            else if (input == "disc")
                                            {
                                                newOrder.ShoppingCart = new();
                                                Console.WriteLine();
                                                Console.WriteLine("Shopping cart reset.");
                                            }
                                            else if (input == "po")
                                            {
                                                if (orderLocation.IsOrderValid(newOrder))
                                                {
                                                    double totalPrice = 0;
                                                    foreach (var item in newOrder.ShoppingCart)
                                                    {
                                                        totalPrice += item.Key.Price;
                                                    }
                                                    newOrder.TotalPrice = totalPrice;
                                                    gameStoreRepository.CreateOrder(newOrder);
                                                    gameStoreRepository.Save();

                                                    foreach (var item in newOrder.ShoppingCart)
                                                    {
                                                        gameStoreRepository.CreateOrderLine(item.Key.Id, item.Value);
                                                        gameStoreRepository.Save();
                                                    }

                                                    Console.WriteLine();
                                                    Console.WriteLine("Order Placed.");
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("Order not valid, location does not have enough quantity");
                                                    Console.WriteLine("Resetting shopping cart");
                                                    newOrder.ShoppingCart = new();
                                                }
                                                break;
                                            }
                                            else if (input == "m")
                                            {
                                                Console.WriteLine();
                                                break;
                                            }
                                            else
                                            {
                                                PrintInvalid(input);
                                            }
                                        }
                                        break;
                                    }
                                    else if (input == "m")
                                    {
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        PrintInvalid(input);
                                    }
                                }
                                break;
                            }
                            else if (input == "m")
                            {
                                Console.WriteLine();
                                break;
                            }
                            else
                            {
                                PrintInvalid(input);
                            }
                        }
                    }
                    //ask for which location (store name in string and search location) **NOTE. Need to implement search location query.
                    //list the location's products and order shopping cart
                    //ask what they want to add to shopping cart and quantity (or if they want to place order)
                    //  check if quantity is <= 3
                    //  check if location's inventory has enough quantity
                    //  if checks pass, add item/quantity to shopping cart
                    //if they place order send data to database (need to insert to Orders and OrderLine tables)
                    //go back to main menu
                }







                else if (input == "nc")
                {
                    Library.Model.Customer customer = new();
                    Console.WriteLine();
                    Console.WriteLine("Creating a new customer.");

                // If first name input is wrong, jump back here
                FirstName:
                    Console.WriteLine();
                    Console.Write("Enter customer's first name (2-20 characters): ");
                    var firstName = Console.ReadLine();
                    Console.WriteLine();
                    try
                    {
                        customer.FirstName = firstName;
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Console.WriteLine();
                        goto FirstName;
                    }

                // If last name input is wrong, jump back here
                LastName:
                    Console.WriteLine();
                    Console.Write("Enter customer's last name (2-20 characters): ");
                    var lastName = Console.ReadLine();
                    Console.WriteLine();
                    try
                    {
                        customer.LastName = lastName;
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Console.WriteLine();
                        goto LastName;
                    }

                // If user id input is wrong, jump back here
                UserName:
                    Console.WriteLine();
                    Console.Write("Enter customer's user id (5-15 characters): ");
                    var userName = Console.ReadLine();
                    Console.WriteLine();
                    try
                    {
                        customer.UserName = userName;
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Console.WriteLine();
                        goto UserName;
                    }

                City:
                    Console.WriteLine();
                    Console.Write("Enter customer's city (2-20 characters): ");
                    var city = Console.ReadLine();
                    Console.WriteLine();
                    try
                    {
                        customer.City = city;
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Console.WriteLine();
                        goto City;
                    }

                State:
                    Console.WriteLine();
                    Console.Write("Enter customer's state (2-20 characters): ");
                    var state = Console.ReadLine();
                    Console.WriteLine();
                    try
                    {
                        customer.State = state;
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Console.WriteLine();
                        goto State;
                    }

                    Console.WriteLine("Are you sure you want to create the following customer?");
                    Console.WriteLine($"Username: {userName}\t\tName: {customer.GetFullName()}\t\tCity: {city}\t\tState: {state}");
                    Console.WriteLine();
                    Console.Write("\"y\" for yes, \"n\" for no: ");
                    input = Console.ReadLine();
                    Console.WriteLine();

                    if (input == "y")
                    {
                        gameStoreRepository.CreateCustomer(customer);
                        gameStoreRepository.Save();
                        Console.WriteLine("Customer Created!");
                        Console.WriteLine();
                    }
                    else if (input == "n")
                    {
                        Console.WriteLine("Customer discarded");
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        PrintInvalid(input);
                    }

                }
                else if (input == "q")
                {
                    break;
                }
                else
                {
                    PrintInvalid(input);
                }
            }
        }

        private static bool IsValidProductName(string name, Dictionary<Library.Model.Product, int> inventory)
        {
            foreach (var item in inventory)
            {
                if (item.Key.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        private static void PrintLocationList(List<Library.Model.Location> allLocations)
        {
            Console.WriteLine("Listing all Locations");
            Console.WriteLine();
            Console.WriteLine("#\t\tId\t\tCity\t\tState");
            for (int i = 1; i <= allLocations.Count; i++)
            {
                Console.WriteLine($"{i}\t\t{allLocations[i - 1].Id}\t\t{allLocations[i - 1].City}\t\t{allLocations[i - 1].State}");
            }
        }

        private static void PrintCustomerList(List<Library.Model.Customer> customerList)
        {
            // if no customer with that name
            if (customerList.Count == 0)
            {
                Console.WriteLine("No customer with that name");
                Console.WriteLine();
            }
            // print all customers with that name
            else
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Choose which customer");
                    Console.WriteLine();
                    Console.WriteLine("#\t\tUserId\t\tName");

                    for (int i = 1; i <= customerList.Count; i++)
                    {

                        Console.WriteLine($"{i}:\t\t{customerList[i - 1].UserName}\t\t{customerList[i - 1].GetFullName()}");
                    }
                    Console.WriteLine();
                    break;
                }
            }
        }

        //private static Location CreateLocation(int num)
        //{
        //    var location = new Location("New York", num);
        //    return location;
        //}

        //private static Customer CreateCustomer()
        //{
        //    var customer = new Customer("hamza1", "Hamza", "Butt");
        //    return customer;
        //}

        private static void PrintInvalid(string input)
        {
            Console.WriteLine();
            Console.WriteLine($"||Invalid input \"{input}\".||");
            Console.WriteLine();
        }

        //private static Order CreateOrder(Customer customer, int num)
        //{
        //    Location location = CreateLocation(num);
        //    var order = new Order(customer, location);
        //    Product product = new("League");
        //    Product product2 = new("Smite");
        //    Product product3 = new("Pizza");
        //    Product product4 = new("COD");
        //    order.AddProduct(product, 2);
        //    order.AddProduct(product2, 3);
        //    order.AddProduct(product3, 4);
        //    order.AddProduct(product4, 5);
        //    return order;
        //}
    }
}
