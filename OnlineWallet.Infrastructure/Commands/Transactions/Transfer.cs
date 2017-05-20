using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Transactions
{
    public class Transfer : IAuthenticatedCommand
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailTo { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "1000000000000")]
        public decimal Amount { get; set; }

        public Guid UserId { get; set; }
    }
}