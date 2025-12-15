using FluentValidation;

using static BookStore.Application.DTOs.Books.Requests.Constants.BookValidationConstants;

namespace BookStore.Application.DTOs.Books.Requests.Validation
{
    internal sealed class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .GreaterThan(0);

            When(x => string.IsNullOrWhiteSpace(x.NewTitle), () =>
            {
                RuleFor(x => x.NewTitle)
                    .Length(TitleMinLength, TitleMaxLength);
            });

            When(x => x.NewAuthorId is not null, () =>
            {
                RuleFor(x => x.NewAuthorId)
                    .NotEqual(Guid.Empty);
            });

            When(x => x.NewPrice is not null, () =>
            {
                RuleFor(x => x.NewPrice)
                    .GreaterThanOrEqualTo(0);
            });
        }
    }
}
