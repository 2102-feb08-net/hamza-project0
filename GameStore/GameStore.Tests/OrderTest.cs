using GameStore.Library.Model;
using System;
using Xunit;

namespace GameStore.Tests
{
    public class OrderTest
    {
        private Product product = new();
        private Order order = new();

        private void SetUp()
        {
            product.Id = 0;
            product.Name = "Banana";
            product.Price = 1.99;
        }
        
        // checks quantity is valid (should pass if it does not throw exception)
        [Fact]
        public void AddProduct_ValidQuantity_DoesNotThrowException()
        {
            SetUp();
            int quantity = 2;
            
            
            order.AddProduct(product, quantity);

        }

        
    }
}
