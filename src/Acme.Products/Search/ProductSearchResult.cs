using System;

namespace Acme.Products.Search
{
    public class ProductSearchResult
    {
        public Guid ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductDiscountAmount { get; set; }
        public string ProductDiscountText { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPriceAmount { get; set; }
        public string ProductPriceText { get; set; }
        public string ProductSku { get; set; }
        public string StockLevel { get; set; }
        public int StockLevelId { get; set; }
    }
}