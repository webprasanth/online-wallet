using System.Collections.Generic;

namespace OnlineWallet.Core.Domain
{
    public class Account
    {
        public decimal Balance { get; protected set; } = 0;

        public IEnumerable<Transaction> Transactions { get; protected set; }
    }
}