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

                    Console.WriteLine();
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

                                Console.WriteLine($"{i}:\t\t{customerList[i-1].UserName}\t\t{customerList[i-1].GetFullName()}");
                            }
                            Console.WriteLine();

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
                        Console.WriteLine("Listing all Locations");
                        Console.WriteLine();
                        Console.WriteLine("#\t\tId\t\tCity\t\tState");
                        for (int i = 1; i <= allLocations.Count; i++)
                        {
                            Console.WriteLine($"{i}\t\t{allLocations[i-1].Id}\t\t{allLocations[i-1].City}\t\t{allLocations[i-1].State}");
                        }

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
