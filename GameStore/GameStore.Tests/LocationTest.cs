using GameStore.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.Tests
{
    public class LocationTest
    {
        private Order order = new();
        private Location location = new();


        [Fact]
        public void IsOrderValid_ValidOrder_ReturnsTrue()
        {
            Product product1 = new();
            product1.Id = 0;
            product1.Name = "Banana";
            product1.Price = 1.99;
            int quantity1 = 2;
            Product product2 = new();
            product2.Id = 1;
            product2.Name = "Apple";
            product2.Price = 0.99;
            int quantity2 = 1;
            order.AddProduct(product1, quantity1);
            order.AddProduct(product2, quantity2);
            location.Inventory.Add(product1, quantity1);
            location.Inventory.Add(product2, quantity2);
            

            Assert.True(location.IsOrderValid(order));
        }
    }
}
