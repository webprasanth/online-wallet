
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Users
{
    public class ChangeAddress : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
