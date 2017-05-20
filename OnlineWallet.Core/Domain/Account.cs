namespace OnlineWallet.Core.Domain
{
    public class Account
    {
        protected Account(decimal balance)
        {
            Balance = balance;
        }

        public decimal Balance { get; }

        public static Account NewAccount(decimal balance)
            => new Account(balance);

       
    }
}