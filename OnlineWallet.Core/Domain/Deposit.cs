namespace OnlineWallet.Core.Domain
{
    public class Deposit : Transaction
    {
        protected Deposit()
        {
        }
        public Deposit(decimal amount, User userFrom) : base(amount, userFrom)
        {
        }
    }
}