using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Model
{
    //need something for time https://docs.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime
    public class Order
    {
        public Customer Customer { get; }
        public Location Location { get; }
        private Dictionary<Product, int> _products = new();
        private DateTimeOffset _timeOrderPlaced;
        

        public Order(Customer customer, Location location)
        {
            Customer = customer;
            Location = location;
        }

        public void PlaceOrder()
        {


            //at the end set the datetime to now
            _timeOrderPlaced = new DateTimeOffset(DateTime.Now);
        }

        public void AddProduct(Product product, int amount)
        {
            _products.Add(product, amount);
        }

        public DateTimeOffset GetTimeOrderPlaced() => _timeOrderPlaced;
        public Dictionary<Product, int> GetProducts() => _products;
    }
}
