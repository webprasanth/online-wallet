using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
    public class GetTransactionsWithDetailsHandler : IQueryHandler<GetTransactionsWithDetails,IEnumerable<TransactionDto>>
    {
        private readonly ITransactionQueries _transactionQueries;

        public GetTransactionsWithDetailsHandler(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        public async Task<IEnumerable<TransactionDto>> RetrieveAsync(GetTransactionsWithDetails query)
        {
            return await _transactionQueries.GetTransactionsWithDetailsAsync(query.UserId);

        }
    }
}