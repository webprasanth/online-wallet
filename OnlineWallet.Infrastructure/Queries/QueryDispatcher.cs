using System;
using System.Threading.Tasks;
using Autofac;

namespace OnlineWallet.Infrastructure.Queries
{
    public sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _context;


        public QueryDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            var queryHandler = GetHandler<IQueryHandler<TQuery, TResult>, TQuery>(query);

            return await queryHandler.RetrieveAsync(query);
        }

        private THandler GetHandler<THandler, TQuery>(TQuery query) where TQuery : IQuery
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var queryHandler = _context.Resolve<THandler>();

            if (queryHandler == null)
                throw new Exception($"No handler found for query '{query.GetType().FullName}'");

            return queryHandler;
        }

    }
}