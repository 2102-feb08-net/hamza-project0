using GameStore.DataAccess.Entities;
using GameStore.DataAccess.Repositories;
using GameStore.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore
{
    class Dependencies : IDesignTimeDbContextFactory<GameStoreContext>, IDisposable
    {
        private bool disposedValue;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();


        public GameStoreContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameStoreContext>();
            var connectionString = File.ReadAllText("E:/Programming/revature/gamestore-connection-string.txt");
            optionsBuilder.UseSqlServer(connectionString);

            return new GameStoreContext(optionsBuilder.Options);
        }

        public IGameStoreRepository CreateGameStoreRepository()
        {
            var dbContext = CreateDbContext();
            _disposables.Add(dbContext);
            return new GameStoreRepository(dbContext);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (IDisposable disposable in _disposables)
                    {
                        disposable.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
