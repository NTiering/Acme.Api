using System.Collections.Generic;

namespace Acme.Data.Search.Product
{
    public class ProductReviewSearchResult : ProductSearchResult
    {
        public IEnumerable<ReviewSearchResult> TopResults { get; internal set; }
        public IEnumerable<ReviewSearchResult> BottomResults { get; internal set; }
        public double ReviewAverage { get; internal set; }
        public int ReviewCount { get; internal set; }
    }
}