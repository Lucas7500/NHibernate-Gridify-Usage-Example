using BookStore.Application.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

using static BookStore.Application.DTOs.Books.Requests.Constants.BookValidationConstants;

namespace BookStore.Application.DTOs.Books.Requests
{
    public record AddBookRequest
    {
        [Required]
        [Length(TitleMinLength, TitleMaxLength)]
        public string Title { get; init; } = string.Empty;

        [Required]
        [NotEmptyGuid]
        public Guid AuthorId { get; init; }
        
        [Required]
        [NonNegative]
        public decimal Price { get; init; }
    }
}
