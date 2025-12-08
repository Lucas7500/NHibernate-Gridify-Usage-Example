using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Contracts;
using BookStore.Domain.ValueObjects;
using ErrorOr;

namespace BookStore.Application.UseCases.Books.Contracts
{
    public interface IGetBookByIdUseCase : IUseCase<BookId, ErrorOr<BookWithAuthorResponse>>;
}
