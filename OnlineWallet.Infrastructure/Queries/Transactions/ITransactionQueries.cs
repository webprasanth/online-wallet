using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Queries.Transactions
{
    public interface ITransactionQueries
    {
        Task<TransactionDto> GetTransactionAsync(Guid id);

        Task<IEnumerable<TransactionDto>> GetAllTransactionsWithDetailsAsync(GetAllTransactionsWithDetails query);

        Task<IEnumerable<TransactionDto>> GetTransactionsWithDetailsAsync(GetTransactionsWithDetails query);

    }
}