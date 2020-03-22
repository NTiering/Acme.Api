using Acme.Data.DataModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Web.Api.Models
{
    public class UpdateStockLevel
    {
        [Required]
        [Range(0,3)]
        public StockLevel StockLevel { get; set; }

        [Required]
        public Guid Id { get; set; }

        public void UpdateDataModel(ProductDataModel dataModel)
        {
            dataModel.StockLevel = StockLevel;
        }
    }
}