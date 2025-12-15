using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Validation.Attributes
{
    public class NonNegativeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal decimalValue && decimalValue < 0)
                return new ValidationResult("Value must be non-negative.");

            return ValidationResult.Success;
        }
    }
}
