using System;

namespace OnlineWallet.Infrastructure.Queries
{
    public class GetTransactionsWithDetails : IQuery
    {
        public Guid UserId { get; set; }
    }
}