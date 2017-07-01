using System;

namespace OnlineWallet.Infrastructure.Queries.Transactions
{
    public class GetDashboardData : IQuery
    {
        public Guid UserId { get; set; }
    }
}