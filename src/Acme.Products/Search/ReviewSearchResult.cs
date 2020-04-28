using System;

namespace Acme.Products.Search
{
    public class ReviewSearchResult
    {
        public DateTime CreatedOn { get; set; }
        public string ReviewText { get; set; }
        public int Score { get; set; }
    }
}