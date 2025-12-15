using FluentValidation;

using static BookStore.Application.DTOs.Authors.Requests.Constants.AuthorValidationConstants;

namespace BookStore.Application.DTOs.Authors.Requests.Validation
{
    internal sealed class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NameMinLength, NameMaxLength);
        }
    }
}
