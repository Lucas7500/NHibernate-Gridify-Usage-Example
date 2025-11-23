using BookStore.Application.DTOs.Authors.Responses;

namespace BookStore.Application.DTOs.Books.Responses
{
    public record BookResponse(
        int Id, 
        string Title, 
        decimal Price, 
        bool IsAvailable, 
        AuthorResponse Author);
}
