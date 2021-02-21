using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Model
{
    public class Location
    {
        private Dictionary<Product, int> _inventory = new();

        public string Name { get; set; }
        public int Id { get => counter; set => counter++; }
        public static int counter = 0;

        //public Location()
        //{

        //}
    }
}
