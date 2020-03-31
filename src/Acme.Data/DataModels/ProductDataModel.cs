using Acme.Data.DataModels.Contracts;
using System;

namespace Acme.Data.DataModels
{
    public class ProductReviewDataModel : BaseDataModel, ICreatedOn
    {
        public int Score { get; set; }
        public string ReviewText { get; set; }
        public Guid ProductId { get; set; }
    }

    public class ProductDataModel : BaseDataModel, ICreatedOn, ILoggable
    {
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }

        public override string LogDescriptorText
        {
            get { return $"Product {Name} Sku {Sku}"; }
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public StockLevel StockLevel { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ProductDataModel model &&
                   Sku == model.Sku &&
                   Name == model.Name &&
                   Description == model.Description &&
                   Price == model.Price &&
                   Discount == model.Discount &&
                   CategoryId.Equals(model.CategoryId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sku, Name, Description, Price, Discount, CategoryId);
        }
    }
}