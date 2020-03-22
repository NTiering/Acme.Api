using Acme.Data.DataModels;
using Acme.Web.Api.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Web.Api.Models
{
    public class AddProductModel
    {
        [Required]
        [IsCatogoryId]
        public Guid CategoryId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal Price { get; set; }

        [Required]
        [CheckSkuNotInUse]
        public string Sku { get; set; }

        [Required]
        public StockLevel StockLevel { get; set; }

        public ProductDataModel ToDataModel()
        {
            var rtn = new ProductDataModel
            {
                CategoryId = this.CategoryId,
                Description = this.Description,
                Discount = this.Discount,
                Name = this.Name,
                Price = this.Price,
                Sku = this.Sku,
                StockLevel = this.StockLevel
            };

            return rtn;
        }
    }
}