using BookStore.Application.DTOs.Authors.Requests;
using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.UseCases.Contracts;
using ErrorOr;

namespace BookStore.Application.UseCases.Books.Contracts
{
    public interface IAddBookUseCase : IUseCase<AddBookRequest, ErrorOr<AddBookResponse>>;
}
