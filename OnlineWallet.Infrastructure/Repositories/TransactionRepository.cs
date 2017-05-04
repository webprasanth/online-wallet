﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;

namespace OnlineWallet.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private static ISet<Transaction> _transactions = new HashSet<Transaction>();

        public async Task<Transaction> GetAsync(Guid id)
        {
            return await Task.FromResult(_transactions.SingleOrDefault(t => t.Id == id));
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
            => await Task.FromResult(_transactions);

        public async Task<IEnumerable<Transaction>> GetAllAsync(User user)
           => await Task.FromResult(_transactions.Where(t => t.UserFrom == user));
        
        public async Task AddAsync(Transaction transaction)
        {
            _transactions.Add(transaction);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            //TO DO
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            var transaction = await GetAsync(id);
           await Task.FromResult(_transactions.Remove(transaction));
        }
    }
}