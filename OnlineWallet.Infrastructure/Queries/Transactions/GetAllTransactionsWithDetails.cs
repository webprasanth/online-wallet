using System;

namespace OnlineWallet.Infrastructure.Queries.Transactions
{
    public class GetAllTransactionsWithDetails : IQuery
    {
        public Guid UserId { get; set; }
    }
}