using BookStore.Application.DTOs.Authors.Responses;

namespace BookStore.Application.DTOs.Books.Responses
{
    public sealed record BookOnlyResponse(
        int Id, 
        string Title, 
        decimal Price, 
        bool IsAvailable);
    
    public sealed record BookWithAuthorResponse(
        int Id, 
        string Title, 
        decimal Price, 
        bool IsAvailable, 
        AuthorResponse Author);
}
