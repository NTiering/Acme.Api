using System;

namespace Acme.Data.Search.Product
{
    public class ReviewSearchResult
    {
        public DateTime CreatedOn { get; internal set; }
        public string ReviewText { get; internal set; }
        public int Score { get; internal set; }
    }
}