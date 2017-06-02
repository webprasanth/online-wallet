using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Core.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetAsync(Guid id);

        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<IEnumerable<Transaction>> GetAllAsync(Guid userId);

        Task<IEnumerable<Transfer>> GetAllTransfersAsync(Guid userid);

        Task<IEnumerable<Deposit>> GetAllDepositsAsync(Guid userid);

        Task<IEnumerable<Withdrawal>> GetAllWithdrawalsAsync(Guid userid);

        Task AddAsync(Transaction transaction);

        Task RemoveAsync(Guid id);
    }
}