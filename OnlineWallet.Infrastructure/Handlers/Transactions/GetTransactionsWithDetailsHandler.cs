using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Transactions;
using X.PagedList;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
    public class GetTransactionsWithDetailsHandler : IQueryHandler<GetTransactionsWithDetails,IPagedList<TransactionDto>>
    {
        private readonly ITransactionQueries _transactionQueries;

        public GetTransactionsWithDetailsHandler(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        public async Task<IPagedList<TransactionDto>> RetrieveAsync(GetTransactionsWithDetails query)
        {
            return await _transactionQueries.GetTransactionsWithDetailsAsync(query);
        }
    }
}