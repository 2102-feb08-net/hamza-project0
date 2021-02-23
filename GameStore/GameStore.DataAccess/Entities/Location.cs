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
        }

        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual ICollection<LocationInventory> LocationInventories { get; set; }
    }
}
