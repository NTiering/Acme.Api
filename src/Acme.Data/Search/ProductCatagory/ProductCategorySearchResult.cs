using System;

namespace Acme.Data.Search.ProductCatagory
{
    public class ProductCategorySearchResult
    {
        public Guid ProductCategoryId { get; internal set; }
        public string ProductCategoryName { get; internal set; }
    }
}