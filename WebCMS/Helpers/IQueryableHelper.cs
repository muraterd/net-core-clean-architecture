using System;
using System.Linq;

namespace WebCMS.Helpers
{
    public class IQueryableHelper
    {
        public IQueryableHelper()
        {
        }
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int currentPage, int itemsPerPage)
        {
            int Skip = (currentPage - 1) * itemsPerPage;
            return queryable.Skip(Skip).Take(itemsPerPage);
        }
    }
}
