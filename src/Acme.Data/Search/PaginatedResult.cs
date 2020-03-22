using System.Collections.Generic;

namespace Acme.Data.Search
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(int pageSize, int pageCount, IEnumerable<T> items, long searchDuration, int totalResults)
        {
            PageSize = pageSize;
            PageCount = pageCount;
            Items = items;
            SearchDuration = searchDuration;
            TotalResults = totalResults;
        }

        public IEnumerable<T> Items { get; }
        public int PageCount { get; }
        public int PageSize { get; }
        public long SearchDuration { get; }
        public int TotalResults { get; }
    }
}