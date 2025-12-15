using FluentValidation;

using static BookStore.Application.DTOs.Books.Requests.Constants.BookValidationConstants;

namespace BookStore.Application.DTOs.Books.Requests.Validation
{
    internal sealed class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(TitleMinLength, TitleMaxLength);

            RuleFor(x => x.AuthorId)
                .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}
