using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Data;

namespace OnlineWallet.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public TransactionRepository(OnlineWalletContext context)
        {
            Context = context;
        }

        public async Task<Transaction> GetAsync(Guid id)
            => await Context.Transactions.SingleOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(Transaction transaction)
        {
           await Context.Transactions.AddAsync(transaction);
        }

        public async Task RemoveAsync(Guid id)
        {
            var transaction = await Context.Transactions.SingleOrDefaultAsync(t => t.Id == id);
            Context.Transactions.Remove(transaction);
        }

        public OnlineWalletContext Context { get; }
    }
}