using System.ComponentModel.DataAnnotations;

using static BookStore.Application.DTOs.Authors.Requests.Constants.AuthorValidationConstants;

namespace BookStore.Application.DTOs.Authors.Requests
{
    public record AddAuthorRequest
    {
        [Required]
        [Length(NameMinLength, NameMaxLength)]
        public string Name { get; init; } = string.Empty;
    }
}
