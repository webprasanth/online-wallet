using System;
using System.Threading.Tasks;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Core.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetAsync(Guid id);

        Task AddAsync(Transaction transaction);

        Task RemoveAsync(Guid id);
    }
}