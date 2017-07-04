using System;
using System.ComponentModel.DataAnnotations;
using OnlineWallet.Infrastructure.Queries.Pagination;

namespace OnlineWallet.Infrastructure.Queries.Transactions
{
    public class GetTransactionsWithDetails : PagedQueryBase, IQuery
    {
        public Guid UserId { get; set; }

        public string Type { get; set; }

        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "invalid value")]
        [Range(1.00, 10000000.00)]
        public string MinAmount { get; set; }

        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "invalid value")]
        [Range(1.00, 10000000.00)]
        public string MaxAmount { get; set; }

    }
}