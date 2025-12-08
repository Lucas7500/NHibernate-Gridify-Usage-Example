using BookStore.Application.DTOs.Books.Responses;

namespace BookStore.Application.DTOs.Authors.Responses
{
    public record UpdateBookResponse(bool IsSuccess, string Message, BookOnlyResponse? UpdatedBook);
}
