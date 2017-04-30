namespace OnlineWallet.Core.Domain
{
    public class Transfer : Transaction
    {
        public Transfer(decimal amount, User userFrom, User userTo) : base(amount, userFrom)
        {
            UserTo = userTo;
        }

        public User UserTo { get; protected set; }
    }
}