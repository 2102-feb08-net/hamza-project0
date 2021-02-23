using System;
using System.Collections.Generic;

#nullable disable

namespace GameStore.DataAccess.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
