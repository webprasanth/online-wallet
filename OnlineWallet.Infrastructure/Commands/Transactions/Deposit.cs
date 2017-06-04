﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Transactions
{
    public class Deposit : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{0,}[,.][0-9]{2}", ErrorMessage = "invalid value")] // decimal with . or ,
        [Range(1.00, 10000000.00)]
        public string Amount { get; set; }
    }
}
