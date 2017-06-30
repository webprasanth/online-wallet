using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}