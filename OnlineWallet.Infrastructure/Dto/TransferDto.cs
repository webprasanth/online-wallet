namespace OnlineWallet.Infrastructure.Dto
{
    public class TransferDto : TransactionDto
    {
        public string UserToEmail { get; set; }
    }
}