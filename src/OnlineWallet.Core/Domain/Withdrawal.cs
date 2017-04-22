namespace OnlineWallet.Core.Domain
{
    public class Withdrawal : Transaction
    {
        public Withdrawal(decimal amount, User userFrom) : base(amount, userFrom)
        {
        }
    }
}