using System.ComponentModel.DataAnnotations;
using RealEstatePrice.Core.Interfaces.Services;

namespace RealEstatePrice.Core.Validations
{
    public class PriceIdValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IValidationService service = (IValidationService)validationContext
                .GetService(typeof(IValidationService));
            bool isValid = service.PriceIdValidate((int)value);
            if (isValid)
            {
                return ValidationResult.Success;
            }
            else 
            {
                return new ValidationResult("PriceId not exists.");
            }
        }

    }
}