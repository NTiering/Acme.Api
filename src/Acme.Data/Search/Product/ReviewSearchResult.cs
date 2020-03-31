using System;

namespace Acme.Data.Search.Product
{
    public class ReviewSearchResult
    {
        public string ReviewText { get; internal set; }
        public int Score { get; internal set; }
        public DateTime CreatedOn { get; internal set; }
    }
}