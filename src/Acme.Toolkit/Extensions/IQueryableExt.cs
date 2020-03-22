using System.Collections.Generic;
using System.Linq;

namespace Acme.Toolkit.Extensions
{
    public static class IQueryableExt
    {
        public static IEnumerable<T> Paginate<T>(this IQueryable<T> items, int pageCount, int pageSize)
        {
            var result = items.Skip(pageCount * pageSize).Take(pageSize);
            return result;
        }
    }
}