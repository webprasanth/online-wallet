using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Pagination;
using OnlineWallet.Infrastructure.Queries.Transactions;
using X.PagedList;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
    public class GetPagedTransactionsWithDetailsHandler : IQueryHandler<GetPagedTransactionsWithDetails,IPagedList<TransactionDto>>
    {
        private readonly ITransactionQueries _transactionQueries;

        public GetPagedTransactionsWithDetailsHandler(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        public async Task<IPagedList<TransactionDto>> RetrieveAsync(GetPagedTransactionsWithDetails query)
        {
            var transactionDtos = await _transactionQueries.GetTransactionsWithDetailsAsync(query.UserId,query.Type, query.Min, query.Max);

            var pagedTransactions = await transactionDtos.PaginateAsync(query.Page, query.ItemsPerPage);

            return pagedTransactions;
        }
    }
}