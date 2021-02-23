using System;
using System.Collections.Generic;

#nullable disable

namespace GameStore.DataAccess.Entities
{
    public partial class Location
    {
        public Location()
        {
            LocationInventories = new HashSet<LocationInventory>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual ICollection<LocationInventory> LocationInventories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
