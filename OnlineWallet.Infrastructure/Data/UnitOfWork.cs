using OnlineWallet.Core;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Repositories;

namespace OnlineWallet.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineWalletContext _context;

        public UnitOfWork(OnlineWalletContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Transactions = new TransactionRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IUserRepository Users { get; }
        public ITransactionRepository Transactions { get; }
        public int Complete()
        {
           return _context.SaveChanges();
        }
    }
}
