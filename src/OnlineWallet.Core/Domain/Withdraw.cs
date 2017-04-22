namespace OnlineWallet.Core.Domain
{
    public class Withdraw : Transaction
    {
        public Withdraw(decimal amount, User userFrom) : base(amount, userFrom)
        {
        }
    }
}