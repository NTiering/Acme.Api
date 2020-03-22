using System;

namespace Acme.Data.Search.Product
{
    public class ProductSearchResult
    {
        public Guid ProductCategoryId { get; internal set; }
        public string ProductCategoryName { get; internal set; }
        public string ProductDescription { get; internal set; }
        public decimal ProductDiscountAmount { get; internal set; }
        public string ProductDiscountText { get; internal set; }
        public Guid ProductId { get; internal set; }
        public string ProductName { get; internal set; }
        public decimal ProductPriceAmount { get; internal set; }
        public string ProductPriceText { get; internal set; }
        public string ProductSku { get; internal set; }
        public string StockLevel { get; internal set; }
        public int StockLevelId { get; internal set; }
    }
}