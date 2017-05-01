using System;

namespace OnlineWallet.Core.Domain
{
    public abstract class Transaction
    {
        protected Transaction()
        {
        }
        protected Transaction(decimal amount,User userFrom)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            UserFrom = userFrom;
            Date = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; } 

        public decimal Amount { get; protected set; }

        public User UserFrom { get; protected set; }

        public DateTime Date { get; protected set; }


    }
}