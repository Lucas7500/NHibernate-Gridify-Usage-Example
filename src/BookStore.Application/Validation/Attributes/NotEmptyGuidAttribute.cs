using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Validation.Attributes
{
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Guid guid && guid == Guid.Empty)
                return new ValidationResult("Guid cannot be empty.");

            return ValidationResult.Success;
        }
    }
}
