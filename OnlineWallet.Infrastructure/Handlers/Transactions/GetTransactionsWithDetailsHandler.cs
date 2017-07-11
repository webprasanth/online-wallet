using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Transactions;

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
            var transactionDtos = await _transactionQueries.GetTransactionsWithDetailsAsync(query.UserId,query.Type, query.Min, query.Max);

            return transactionDtos;
        }
    }
}