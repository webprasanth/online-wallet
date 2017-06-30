using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Queries
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> RetrieveAsync(TQuery query);
    }
}