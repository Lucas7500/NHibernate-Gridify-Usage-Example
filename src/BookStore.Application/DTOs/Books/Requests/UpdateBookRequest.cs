using BookStore.Application.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs.Books.Requests
{
    public sealed record UpdateBookRequest
    {
        [Required]
        [IntegerId]
        public int BookId { get; init; }

        public string? NewTitle { get; init; }

        public Guid? NewAuthorId { get; init; }

        public decimal? NewPrice { get; init; }

        public bool? NewIsAvailable { get; init; }
    }
}