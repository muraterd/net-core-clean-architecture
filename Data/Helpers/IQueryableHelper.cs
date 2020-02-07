using Data.Models.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

public class IQueryableHelper
{
    public IQueryableHelper()
    {
    }
}

public static class IQueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int page, int pageSize)
    {
        int Skip = (page - 1) * pageSize;
        return queryable.Skip(Skip).Take(pageSize);
    }

    public static int CalculateTotalPageCount(this int totalItemCount, int pageSize)
    {
        return Convert.ToInt32(Math.Ceiling((totalItemCount / (decimal)pageSize)));
    }
}