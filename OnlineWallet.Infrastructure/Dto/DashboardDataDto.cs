namespace OnlineWallet.Infrastructure.Dto
{
    public class DashboardDataDto
    {
        public int DepositsCount { get; set; }
        public int WithdrawalsCount { get; set; }
        public int TransfersCount { get; set; }
        public decimal Incomes { get; set; }
        public decimal Outcomes { get; set; }
    }
}