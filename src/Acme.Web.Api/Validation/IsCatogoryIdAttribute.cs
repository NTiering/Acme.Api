using Acme.Data.Context;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Acme.Web.Api.Validation
{
    public class IsCatogoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var catId = (Guid)value;
            var service = validationContext.GetService(typeof(ISearchContext)) as ISearchContext;
            if (service.ProductCategories.Any(x => x.Id == catId) == false)
            {
                return new ValidationResult($"Not a valid category Id");
            }

            return ValidationResult.Success;
        }
    }
}