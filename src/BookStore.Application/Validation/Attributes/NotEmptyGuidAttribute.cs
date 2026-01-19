using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class NotEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Guid guid && guid == Guid.Empty)
                return new ValidationResult("Guid cannot be empty.");

            return ValidationResult.Success;
        }
    }
}
