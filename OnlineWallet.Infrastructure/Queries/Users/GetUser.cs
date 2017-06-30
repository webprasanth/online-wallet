using System;

namespace OnlineWallet.Infrastructure.Queries.Users
{
    public class GetUser : IQuery
    {
        public Guid UserId { get; set; }
    }
}