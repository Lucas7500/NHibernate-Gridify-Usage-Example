using BookStore.Application.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs.Authors.Requests
{
    public record UpdateAuthorRequest
    {
        [Required]
        [NotEmptyGuid]
        public Guid AuthorId { get; init; }
        
        public string? NewName { get; init; }
    }
}
