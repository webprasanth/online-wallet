using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Users
{
    public class ChangePassword : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Password must be between 6 and 32 characters", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
