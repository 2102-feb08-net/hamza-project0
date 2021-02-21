using System;
using System.Collections.Generic;
using System.Text;
using GameStore.Library.Interfaces;
using GameStore.Library.Model;

namespace GameStore.DataAccess.Repositories
{
    public class GameStoreRepository : IGameStoreRepository
    {
        // make sure to order by time
        public IEnumerable<Order> GetCustomerOrderHistory(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Library.Model.Customer> SearchCustomerName()
        {
            throw new NotImplementedException();
        }
    }
}
