using BookStore.Application.DTOs.Books.Responses;
using BookStore.Domain.Models.BookModel;

namespace BookStore.Application.Mappers
{
    internal static class BooksMapperExtensions
    {
        public static BookResponse ToResponse(this Book book)
        {
            return new BookResponse
            (
                Id: book.Id.Value,
                Title: book.Title,
                Price: book.Price,
                IsAvailable: book.IsAvailable,
                Author: book.Author.ToResponse());
        }
    }
}
