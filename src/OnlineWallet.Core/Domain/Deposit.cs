namespace OnlineWallet.Core.Domain
{
    public class Deposit : Transaction
    {
        public Deposit(decimal amount, User userFrom) : base(amount, userFrom)
        {
        }
    }
}