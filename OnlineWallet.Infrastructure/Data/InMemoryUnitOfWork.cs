using System;
using OnlineWallet.Core;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Repositories;

namespace OnlineWallet.Infrastructure.Data
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public InMemoryUnitOfWork()
        {
            Saved = false;
            Users = new InMemoryUserRepository();
            Transactions = new InMemoryTransactionRepository();
        }

        public IUserRepository Users { get; set; }
        public ITransactionRepository Transactions { get; set; }
        public bool Saved { get; private set; }

        public int Save()
        {
            Saved = true;

            return 1;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
