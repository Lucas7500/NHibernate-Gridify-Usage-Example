using BookStore.Application.DTOs.Authors.Responses;

namespace BookStore.Application.DTOs.Books.Responses
{
    public record BookOnlyResponse(
        int Id, 
        string Title, 
        decimal Price, 
        bool IsAvailable);
    
    public record BookWithAuthorResponse(
        int Id, 
        string Title, 
        decimal Price, 
        bool IsAvailable, 
        AuthorResponse Author);
}
