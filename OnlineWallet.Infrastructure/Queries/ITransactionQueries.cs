using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Queries
{
    public interface ITransactionQueries
    {
        Task<TransactionDto> GetTransactionAsync(Guid id);

        Task<IEnumerable<TransactionDto>> GetTransactionsWithDetailsAsync(Guid userId);
    }
}