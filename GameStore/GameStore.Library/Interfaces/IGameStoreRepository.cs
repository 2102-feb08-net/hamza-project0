using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Library.Model;

namespace GameStore.Library.Interfaces
{
    public interface IGameStoreRepository
    {


        IEnumerable<Library.Model.Customer> SearchCustomerName();

        IEnumerable<Library.Model.Order> GetCustomerOrderHistory(Customer customer);
    }
}
