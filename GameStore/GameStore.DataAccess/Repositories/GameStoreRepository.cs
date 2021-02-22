using System;
using System.Collections.Generic;
using System.Text;
using GameStore.Library.Interfaces;
using GameStore.Library.Model;

namespace GameStore.DataAccess.Repositories
{
    public class GameStoreRepository : IGameStoreRepository
    {
        public void CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        // order by name
        public IEnumerable<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        // make sure to order by time
        public IEnumerable<Order> GetCustomerOrderHistory(Customer customer)
        {
            throw new NotImplementedException();
        }

        // order by time
        public IEnumerable<Order> GetLocationOrderHistory(Location location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Library.Model.Customer> SearchCustomerName()
        {
            throw new NotImplementedException();
        }
    }
}
