using System;
using System.Collections.Generic;
using System.Linq;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;

namespace OnlineWallet.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private static ISet<Transaction> _transactions = new HashSet<Transaction>();

        public Transaction Get(Guid id)
        {
            return _transactions.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetAll()
            => _transactions;

        public void Add(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void Update(Transaction transaction)
        {
            //TO DO
        }

        public void Remove(Guid id)
        {
            var transaction = Get(id);
            _transactions.Remove(transaction);
        }
    }
}