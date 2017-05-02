using System.ComponentModel.DataAnnotations;
namespace OnlineWallet.Infrastructure.Commands.User
{
    public class CreateUser : ICommand
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(32,MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

    }
}