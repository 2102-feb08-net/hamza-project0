using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Model
{
    public class Location
    {
        private Dictionary<Product, int> _inventory = new();
        public string Name { get;}
        public int Id { get; }

        public Location(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public void AddProduct(Product product, int amount)
        {
            _inventory.Add(product, amount);
        }

        public Dictionary<Product, int> GetInventory() => _inventory;



    }
}
