using System;

namespace OnlineWallet.Core.Domain
{
    public class Account
    {
        protected Account()
        {

        }

        public Account(decimal balance)
        {
            Balance = balance;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
        public decimal Balance { get; protected set; }

        public void SetBalance(decimal value)
        {
            if (value < 0)
            {
                throw new InvalidOperationException("Cannot set balance to negative");
            }
            else
            {
                 Balance = value;
            }
        }

    }
}