using System.ComponentModel.DataAnnotations;
namespace OnlineWallet.Infrastructure.Commands.Users
{
    public class CreateUser : ICommand
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

    }
}