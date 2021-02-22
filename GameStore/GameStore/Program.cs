using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.Repositories;
using GameStore.Library.Interfaces;
using GameStore.Library.Model;

namespace GameStore
{
    class Program
    {
        static void Main(string[] args)
        {

            IGameStoreRepository gameStoreRepository = new GameStoreRepository();
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
                    //var name = input.Split(' ', 2)[1];
                    //var customerList = gameStoreRepository.SearchCustomerName().ToList();
                    var customerList = new List<Customer>();
                    customerList.Add(CreateCustomer()); //testing
                    customerList.Add(CreateCustomer()); //testing
                    customerList.Add(CreateCustomer()); //testing

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

                            for (int i = 0; i < customerList.Count; i++)
                            {

                                Console.WriteLine($"{i}:\t\t{customerList[i].UserId}\t\t{customerList[i].GetFullName()}");
                            }
                            Console.WriteLine();

                            Console.Write("Pick a customer number to show their order history, or \"m\" to go to main menu: ");
                            input = Console.ReadLine();

                            // valid customer #, print their history
                            if (int.TryParse(input, out var customerNum)
                                    && customerNum >= 0 && customerNum < customerList.Count)
                            {
                                Customer customer = customerList[customerNum];
                                //var customerOrderHistory = gameStoreRepository.GetCustomerOrderHistory(customer).ToList();
                                var customerOrderHistory = new List<Order>(); //testing
                                customerOrderHistory.Add(CreateOrder(customer, 0)); //testing
                                customerOrderHistory.Add(CreateOrder(customer, 1)); // testing
                                customerOrderHistory.Add(CreateOrder(customer, 2)); //testing 

                                Console.WriteLine();
                                Console.WriteLine($"This is the order history of {customer.GetFullName()}:");
                                Console.WriteLine();

                                for (int i = 0; i < customerOrderHistory.Count(); i++)
                                {
                                    Console.WriteLine($"Order {i}\t\tLocation: {customerOrderHistory[i].Location.Id}\t" +
                                        $"Time: {customerOrderHistory[i].GetTimeOrderPlaced()}");
                                    Console.WriteLine();
                                    Console.WriteLine("Product:\tQuantity:");
                                    foreach (KeyValuePair<Product, int> product in customerOrderHistory[i].GetShoppingCart())
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

                    //var allLocations = gameStoreRepository.GetAllLocations().ToList();
                    var allLocations = new List<Location>(); //testing
                    allLocations.Add(CreateLocation(0)); //testing
                    allLocations.Add(CreateLocation(1)); // testing
                    allLocations.Add(CreateLocation(2)); //testing 

                    while (true)
                    {
                        Console.WriteLine("Listing all Locations");
                        Console.WriteLine();
                        Console.WriteLine("#\tId\tName");
                        for (int i = 0; i < allLocations.Count; i++)
                        {
                            Console.WriteLine($"{i}\t{allLocations[i].Id}\t{allLocations[i].Name}");
                        }

                        Console.Write("Pick a location number to show its order history, or \"m\" to go to main menu: ");
                        input = Console.ReadLine();
                        // valid location number
                        if (int.TryParse(input, out var locationNum)
                                        && locationNum >= 0 && locationNum < allLocations.Count)
                        {
                            Location location = allLocations[locationNum];
                            //var locationOrderHistory = gameStoreRepository.GetLocationOrderHistory(location).ToList();
                            var locationOrderHistory = new List<Order>(); //testing
                            Customer customer = CreateCustomer();
                            locationOrderHistory.Add(CreateOrder(customer, 0)); //testing
                            locationOrderHistory.Add(CreateOrder(customer, 1)); // testing
                            locationOrderHistory.Add(CreateOrder(customer, 2)); //testing 


                            Console.WriteLine();
                            Console.WriteLine($"This is the order history of store located in {location.Name}:");
                            Console.WriteLine();

                            for (int i = 0; i < locationOrderHistory.Count(); i++)
                            {
                                Console.WriteLine($"Order {i}\t\tCustomer: {locationOrderHistory[i].Customer.GetFullName()}\t" +
                                    $"Time: {locationOrderHistory[i].GetTimeOrderPlaced()}");
                                Console.WriteLine();
                                Console.WriteLine("Product:\tQuantity:");
                                foreach (KeyValuePair<Product, int> product in locationOrderHistory[i].GetShoppingCart())
                                {
                                    Console.WriteLine($"{product.Key.Name}\t\t{product.Value}");
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
                    Customer customer = new Customer();
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
                UserId:
                    Console.WriteLine();
                    Console.Write("Enter customer's user id (5-15 characters): ");
                    var userId = Console.ReadLine();
                    Console.WriteLine();
                    try
                    {
                        customer.UserId = userId;
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Console.WriteLine();
                        goto UserId;
                    }

                    Console.WriteLine("Are you sure you want to create the following customer?");
                    Console.WriteLine($"UserId: {userId}\t\tName: {customer.GetFullName()}");
                    Console.WriteLine();
                    Console.Write("\"y\" for yes, \"n\" for no: ");
                    input = Console.ReadLine();
                    Console.WriteLine();

                    if (input == "y")
                    {
                        //gameStoreRepository.CreateCustomer(customer);
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

        private static Location CreateLocation(int num)
        {
            var location = new Location("New York", num);
            return location;
        }

        private static Customer CreateCustomer()
        {
            var customer = new Customer("hamza1", "Hamza", "Butt");
            return customer;
        }

        private static void PrintInvalid(string input)
        {
            Console.WriteLine();
            Console.WriteLine($"||Invalid input \"{input}\".||");
            Console.WriteLine();
        }

        private static Order CreateOrder(Customer customer, int num)
        {
            Location location = CreateLocation(num);
            var order = new Order(customer, location);
            Product product = new("League");
            Product product2 = new("Smite");
            Product product3 = new("Pizza");
            Product product4 = new("COD");
            order.AddProduct(product, 2);
            order.AddProduct(product2, 3);
            order.AddProduct(product3, 4);
            order.AddProduct(product4, 5);
            return order;
        }
    }
}
