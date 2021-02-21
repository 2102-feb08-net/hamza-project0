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
                        Console.WriteLine("Choose which customer");
                        Console.WriteLine();
                        Console.WriteLine("#\t\tUserId\t\tFirst\t\tLast");

                        for (int i = 0; i < customerList.Count; i++)
                        {

                            Console.WriteLine($"{i}:\t\t{customerList[i].UserId}\t\t{customerList[i].FirstName}\t\t{customerList[i].LastName}");
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
                            customerOrderHistory.Add(CreateOrder(customer)); //testing
                            customerOrderHistory.Add(CreateOrder(customer)); // testing
                            customerOrderHistory.Add(CreateOrder(customer)); //testing 

                            Console.WriteLine();
                            Console.WriteLine($"This is the order history of {customer.FirstName} {customer.LastName}:");
                            Console.WriteLine();

                            for (int i = 0; i < customerOrderHistory.Count(); i++)
                            {
                                Console.WriteLine($"Order {i}\t\tLocation: {customerOrderHistory[i].Location.Id}\t" +
                                    $"Time: {customerOrderHistory[i].GetTimeOrderPlaced()}");
                                Console.WriteLine();
                                Console.WriteLine("Product:\tQuantity:");
                                foreach (KeyValuePair<Product, int> product in customerOrderHistory[i].GetProducts())
                                {
                                    Console.WriteLine($"{product.Key.Name}\t\t{product.Value}");
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                }
                else if (input == "ll")
                {

                }
                else if (input == "o")
                {

                }
                else if (input == "nc")
                {

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

        private static Customer CreateCustomer()
        {
            var customer = new Customer("hamza1", "Hamza", "Butt");
            return customer;
        }

        private static void PrintInvalid(string input)
        {
            Console.WriteLine();
            Console.WriteLine($"Invalid input \"{input}\".");
        }

        private static Order CreateOrder(Customer customer)
        {
            Location location = new();
            location.Name = "New York";
            location.Id = Location.counter;
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
