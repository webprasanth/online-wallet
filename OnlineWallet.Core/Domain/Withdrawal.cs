namespace OnlineWallet.Core.Domain
{
    public class Withdrawal : Transaction
    {
        protected Withdrawal()
        {
        }
        public Withdrawal(decimal amount, User userFrom) : base(amount, userFrom)
        {
        }
    }
}