using Acme.Data.Context;
using Acme.Data.Search.Product;
using Acme.Web.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Acme.Web.Api.Validation
{
    public class CheckSkuNotInUseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationModel = validationContext.ObjectInstance as AddProductModel;
            var service = validationContext.GetService(typeof(ISearchContext)) as ISearchContext;

            var isInUse = service.IsSkuInUse(validationModel.Sku);

            if (isInUse)
            {
                return new ValidationResult($"Sku {isInUse} is in use");
            }

            return ValidationResult.Success;
        }
    }
}