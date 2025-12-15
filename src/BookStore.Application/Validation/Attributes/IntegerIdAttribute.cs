using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Validation.Attributes
{
    public class IntegerIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int id && id <= 0)
                return new ValidationResult("Integer Id cannot be negative or zero.");

            return ValidationResult.Success;
        }
    }
}
