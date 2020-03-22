using Acme.Data.DataModels;
using Acme.Web.Api.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Web.Api.Models
{
    public class UpdateDiscount
    {
        [Required]
        [Range(1, 10)]
        [CheckMinDiscount]
        public decimal Discount { get; set; }

        [Required]
        public Guid Id { get; set; }

        public void UpdateDataModel(ProductDataModel dataModel)
        {
            dataModel.Discount = Discount;
        }
    }
}