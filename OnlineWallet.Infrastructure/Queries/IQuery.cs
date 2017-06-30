using System;

namespace OnlineWallet.Infrastructure.Queries
{
    public interface IQuery
    {
        Guid UserId { get; set; }
    }
}