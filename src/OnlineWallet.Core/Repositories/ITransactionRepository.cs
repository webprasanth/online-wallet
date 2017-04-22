using System;
using System.Collections.Generic;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Core.Repositories
{
    public interface ITransactionRepository
    {
        Transaction Get(Guid id);
        IEnumerable<Transaction> GetAll();

        void Add(Transaction transaction);

        void Update(Transaction transaction);

        void Remove(Guid id);
    }
}