using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<Product> Products { get; set; }
        public List<int> ProductQuanties { get; set; }
        public Dictionary<Product, int> Inventory { get; set; } = new();


        //public Location(int id, string city, string state)
        //{
        //    Id = id;
        //    City = city;
        //    State = state;
        //}

        public void BuildInventory()
        {
            for(int i = 0; i < Products.Count; i++)
            {
                Inventory.Add(Products[i], ProductQuanties[i]);
            }
        }


    }
}
