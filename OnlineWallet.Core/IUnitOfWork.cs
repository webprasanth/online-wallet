using System;
using OnlineWallet.Core.Repositories;

namespace OnlineWallet.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITransactionRepository Transactions { get; }

        int Complete();

    }
}