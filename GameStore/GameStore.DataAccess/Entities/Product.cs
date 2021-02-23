using System;
using System.Collections.Generic;

#nullable disable

namespace GameStore.DataAccess.Entities
{
    public partial class Product
    {
        public Product()
        {
            LocationInventories = new HashSet<LocationInventory>();
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<LocationInventory> LocationInventories { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
