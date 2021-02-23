using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStore.DataAccess.Entities;
using GameStore.Library.Interfaces;
using GameStore.Library.Model;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Repositories
{
    public class GameStoreRepository : IGameStoreRepository
    {

        private readonly GameStoreContext _dbContext;

        public GameStoreRepository(GameStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void CreateCustomer(Library.Model.Customer customer)
        {

            var newCustomer = new Entities.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                City = customer.City,
                State = customer.State
            };
            _dbContext.Add(newCustomer);
        }

        // order by name
        public IEnumerable<Library.Model.Location> GetAllLocations()
        {

            IQueryable<Entities.Location> locations = _dbContext.Locations
                .Include(l => l.LocationInventories)
                    .ThenInclude(ln => ln.Product)
                .AsNoTracking();

            return locations.Select(ln => new Library.Model.Location
            {
                Id = ln.Id,
                City = ln.City,
                State = ln.State,
                Products = ln.LocationInventories.Select(lni => lni.Product).Select(p => new Library.Model.Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList(),
                ProductQuanties = ln.LocationInventories.Select(lni => lni.Quantity).ToList()
            });
        }

        // make sure to order by time
        public IEnumerable<Library.Model.Order> GetCustomerOrderHistory(Library.Model.Customer customer)
        {
            IQueryable<Entities.Order> orders = _dbContext.Orders
                .Select(o => o)
                .Where(o => o.CustomerId == customer.Id)
                .OrderBy(o => o.TimePlaced)
                .AsNoTracking();




            return orders.Select(or => new Library.Model.Order
            {
                Id = or.Id,
                CustomerId = or.CustomerId,
                LocationId = or.LocationId,
                TimePlaced = or.TimePlaced,
                TotalPrice = or.TotalPrice,
                Products = or.OrderLines.Select(orl => orl.Product).Select(p => new Library.Model.Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList(),
                ProductQuanties = or.OrderLines.Select(orl => orl.Quantity).ToList()
            });
        }

        // order by time
        public IEnumerable<Library.Model.Order> GetLocationOrderHistory(Library.Model.Location location)
        {
            IQueryable<Entities.Order> orders = _dbContext.Orders
                .Select(o => o)
                .Where(o => o.LocationId == location.Id)
                .OrderBy(o => o.TimePlaced)
                .AsNoTracking();




            return orders.Select(or => new Library.Model.Order 
            {
                Id = or.Id,
                CustomerId = or.CustomerId,
                LocationId = or.LocationId,
                TimePlaced = or.TimePlaced,
                TotalPrice = or.TotalPrice,
                Products = or.OrderLines.Select(orl => orl.Product).Select(p => new Library.Model.Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList(),
                ProductQuanties = or.OrderLines.Select(orl => orl.Quantity).ToList()
            });
        }

        public IEnumerable<Library.Model.Customer> SearchCustomerName(string name)
        {
            IQueryable<Entities.Customer> customers = _dbContext.Customers
                .Select(c => c)
                .Where(c => (c.FirstName + " " + c.LastName) == name)
                .AsNoTracking();

            return customers.Select(cs => new Library.Model.Customer
            {
                Id = cs.Id,
                FirstName = cs.FirstName,
                LastName = cs.LastName,
                UserName = cs.UserName,
                City = cs.City,
                State = cs.State
            });
        }

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
