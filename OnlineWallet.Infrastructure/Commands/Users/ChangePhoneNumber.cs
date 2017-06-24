
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Users
{
    public class ChangePhoneNumber : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }

        [Required]
        [Range(0,999999999,ErrorMessage = "Number must consist of up to 9 digits")]
        public string Number { get; set; }
    }
}
