using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Transactions;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
    public class GetAllTransactionsWithDetailsHandler : IQueryHandler<GetAllTransactionsWithDetails,IEnumerable<TransactionDto>>
    {
        private readonly ITransactionQueries _transactionQueries;

        public GetAllTransactionsWithDetailsHandler(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        public async Task<IEnumerable<TransactionDto>> RetrieveAsync(GetAllTransactionsWithDetails query)
        {
            return await _transactionQueries.GetAllTransactionsWithDetailsAsync(query.UserId);
        }
    }
}