using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Infrastructure.Commands.Users
{
    public class Login : ICommand
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must have 6-32 characters.")]
        public string Password { get; set; }

        public Guid TokenId { get; set; }
    }
}
