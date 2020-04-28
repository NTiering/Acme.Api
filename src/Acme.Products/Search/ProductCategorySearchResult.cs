using System;

namespace Acme.Products.Search
{
    public class ProductCategorySearchResult
    {
        public Guid ProductCategoryId { get; internal set; }
        public string ProductCategoryName { get; internal set; }
    }
}