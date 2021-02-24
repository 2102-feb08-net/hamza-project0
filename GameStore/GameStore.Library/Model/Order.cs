using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Model
{
    //need something for time https://docs.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime TimePlaced { get; set; }
        public double TotalPrice { get; set; }
        public List<Product> Products { get; set; }
        public List<int> ProductQuanties { get; set; }

        public Dictionary<Product, int> ShoppingCart { get; set; } = new();


        //public Order(Customer customer, Location location)
        //{
        //    Customer = customer;
        //    Location = location;
        //}

        public void BuildShoppingCart()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                ShoppingCart.Add(Products[i], ProductQuanties[i]);
            }
        }

        public void PlaceOrder()
        {


            //at the end set the datetime to now
            //TimePlaced = new DateTime(DateTime.Now);
        }


        /// <summary>
        /// Adds the product and quantity to the shopping cart.
        /// Checks if the quantity is greater than 3. If it is, throws an ArgumentException.
        /// </summary>
        public void AddProduct(Product product, int quantity)
        {
            if (quantity > 3)
            {
                throw new ArgumentException("Quantity cannot be larger than 3.");
            }
            else
            {
                ShoppingCart.Add(product, quantity);
            }
        }

    }
}
