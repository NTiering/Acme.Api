using Acme.Data.Context;
using Acme.Products.Search;
using Acme.Web.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Acme.Web.Api.Validation
{
    public class CheckMinDiscountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationModel = validationContext.ObjectInstance as UpdateDiscount;
            var service = validationContext.GetService(typeof(ISearchContext)) as ISearchContext;

            var searchModel = service.Get(validationModel.Id);

            if (searchModel == null) return ValidationResult.Success;
            if (searchModel.ProductPriceAmount < validationModel.Discount)
            {
                return new ValidationResult($"Discount cannot be greater than the current price of {searchModel.ProductPriceAmount}");
            }

            return ValidationResult.Success;
        }
    }
}