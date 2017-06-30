namespace OnlineWallet.Infrastructure.Queries.Pagination
{
    public abstract class PagedQueryBase
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}