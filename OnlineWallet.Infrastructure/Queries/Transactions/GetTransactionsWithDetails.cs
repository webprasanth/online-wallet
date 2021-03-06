﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Queries.Transactions
{
    public class GetTransactionsWithDetails : IQuery
    {
        public Guid UserId { get; set; }

        public string Type { get; set; }

        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "invalid value")]
        [Range(1.00, 10000000.00)]
        public string Min { get; set; }

        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "invalid value")]
        [Range(1.00, 10000000.00)]
        public string Max { get; set; }

    }
}