using System.Collections.Generic;

namespace Acme.Products.Search
{
    public class ProductReviewSearchResult
    {
        public ProductSearchResult Product { get; set; }
        public IEnumerable<ReviewSearchResult> BottomResults { get; set; }
        public double ReviewAverage { get; set; }
        public int ReviewCount { get; set; }
        public IEnumerable<ReviewSearchResult> TopResults { get; set; }
    }
}