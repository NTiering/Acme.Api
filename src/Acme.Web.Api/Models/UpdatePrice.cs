using Acme.Data.DataModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Web.Api.Models
{
    public class UpdatePrice
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal Price { get; set; }

        public void UpdateDataModel(ProductDataModel dataModel)
        {
            dataModel.Price = Price;
        }
    }
}