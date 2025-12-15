using FluentValidation;

using static BookStore.Application.DTOs.Authors.Requests.Constants.AuthorValidationConstants;

namespace BookStore.Application.DTOs.Authors.Requests.Validation
{
    internal sealed class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .NotEqual(Guid.Empty);

            When(x => !string.IsNullOrWhiteSpace(x.NewName), () =>
            {
                RuleFor(x => x.NewName)
                    .Length(NameMinLength, NameMaxLength);
            });
        }
    }
}
