using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library
{
    public class Location
    {
        private Dictionary<Product, int> _inventory = new();

        public string Name { get; set; }
        public int Id { get; set; }

    }
}
