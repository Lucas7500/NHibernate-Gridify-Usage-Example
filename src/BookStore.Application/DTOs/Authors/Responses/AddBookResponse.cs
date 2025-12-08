using BookStore.Application.DTOs.Books.Responses;

namespace BookStore.Application.DTOs.Authors.Responses
{
    public record AddBookResponse(BookWithAuthorResponse CreatedBook);
}
