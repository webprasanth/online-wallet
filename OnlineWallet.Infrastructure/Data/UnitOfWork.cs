using System;
using OnlineWallet.Core;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Repositories;

namespace OnlineWallet.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineWalletContext _context;
        private bool _disposed = false;

        public UnitOfWork(OnlineWalletContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Transactions = new TransactionRepository(_context);
        }


        public IUserRepository Users { get; }
        public ITransactionRepository Transactions { get; }
        public int Save()
        {
           return _context.SaveChanges();
        }

        

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
