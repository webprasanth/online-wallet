﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Transactions
{
    public class Transfer : IAuthenticatedCommand
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailTo { get; set; }

        [Required]
        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "invalid value")]
        [Range(1.00, 10000000.00)]
        public string Amount { get; set; }

        public Guid UserId { get; set; }
    }
}