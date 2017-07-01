using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineWallet.Infrastructure.Queries.Pagination
{
    public static class Pagination
    {
        public static async Task<IPagedList<T>> PaginateAsync<T>(this IEnumerable<T> collection, int pageNumber,
            int itemsPerPage)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (itemsPerPage < 1)
            {
                itemsPerPage = 20;
            }

            return await collection.ToPagedListAsync(pageNumber, itemsPerPage);
        }
    }
}