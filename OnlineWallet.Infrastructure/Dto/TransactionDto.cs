using System;

namespace OnlineWallet.Infrastructure.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public string UserTo { get; set; }
    }
}