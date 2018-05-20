using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebApiTest4.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> expression)
        {
            if (condition)
            {
                return query.Where(expression);
            }

            return query;
        }
    }
}