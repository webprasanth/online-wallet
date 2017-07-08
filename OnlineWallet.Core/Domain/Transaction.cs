using System;
using static OnlineWallet.Core.Domain.ErrorCodes;

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
            Amount = SetAmount(amount);
            UserFrom = userFrom;
            Date = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; } 

        public decimal Amount { get; protected set; }

        public User UserFrom { get; protected set; }

        public DateTime Date { get; protected set; }

        private static decimal SetAmount(decimal amount)
        {
            if (amount < 1 || amount > 10000000) // 1 - 1,000,000
            {
                throw new DomainException(InvalidTransactionsAmount,"Amount of transaction must be between 1 and 1,000,000");
            }
            return amount;
        }
    }
}