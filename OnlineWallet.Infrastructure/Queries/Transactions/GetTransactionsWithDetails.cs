﻿using System;
using OnlineWallet.Infrastructure.Queries.Pagination;

namespace OnlineWallet.Infrastructure.Queries.Transactions
{
    public class GetTransactionsWithDetails : PagedQueryBase, IQuery
    {
        public Guid UserId { get; set; }
    }
}