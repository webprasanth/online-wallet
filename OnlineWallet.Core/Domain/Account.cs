using System;

namespace OnlineWallet.Core.Domain
{
    public class Account
    {

        protected Account(decimal balance)
        {
            Balance = balance;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
        public decimal Balance { get; protected set; }

        public static Account NewAccount(decimal balance)
            => new Account(balance);

       
    }
}