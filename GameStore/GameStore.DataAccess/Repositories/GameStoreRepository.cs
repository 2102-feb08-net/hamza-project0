using System;
using System.Collections.Generic;
using System.Text;
using GameStore.DataAccess.Entities;
using GameStore.Library.Interfaces;
using GameStore.Library.Model;

namespace GameStore.DataAccess.Repositories
{
    public class GameStoreRepository : IGameStoreRepository
    {
        public void CreateCustomer(Library.Model.Customer customer)
        {
            throw new NotImplementedException();
        }

        // order by name
        public IEnumerable<Library.Model.Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        // make sure to order by time
        public IEnumerable<Library.Model.Order> GetCustomerOrderHistory(Library.Model.Customer customer)
        {
            throw new NotImplementedException();
        }

        // order by time
        public IEnumerable<Library.Model.Order> GetLocationOrderHistory(Library.Model.Location location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Library.Model.Customer> SearchCustomerName()
        {
            throw new NotImplementedException();
        }
    }
}
