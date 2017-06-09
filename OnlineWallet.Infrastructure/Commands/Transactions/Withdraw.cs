using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Transactions
{
    public class Withdraw : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }

        [Required]
        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "invalid value")]
        [Range(1.00,10000000.00)]
        public string Amount { get; set; }
    }
}
