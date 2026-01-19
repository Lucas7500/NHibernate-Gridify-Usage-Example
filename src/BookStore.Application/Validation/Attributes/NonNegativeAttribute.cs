using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class NonNegativeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal decimalValue && decimalValue < 0)
                return new ValidationResult("Value must be non-negative.");

            return ValidationResult.Success;
        }
    }
}
