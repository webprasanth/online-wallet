using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Transactions
{
    public class Deposit : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "1000000000000")]
        public decimal Amount { get; set; }
    }
}
