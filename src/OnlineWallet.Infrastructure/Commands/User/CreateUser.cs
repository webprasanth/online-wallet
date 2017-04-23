namespace OnlineWallet.Infrastructure.Commands.User
{
    public class CreateUser : ICommand
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

    }
}