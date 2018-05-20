using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiTest4.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> func)
        {
            if (condition)
            {
                return query.Where(func);
            }

            return query;
        }
    }
}