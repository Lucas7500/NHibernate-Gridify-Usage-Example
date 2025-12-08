using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs.Authors.Requests
{
    public record AddBookRequest(
        [Required] string Title,
        [Required] Guid AuthorId,
        [Required] decimal Price);
}
