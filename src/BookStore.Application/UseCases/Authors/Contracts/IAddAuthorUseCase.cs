using BookStore.Application.DTOs.Authors.Requests;
using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.UseCases.Contracts;

namespace BookStore.Application.UseCases.Authors.Contracts
{
    public interface IAddAuthorUseCase : IUseCase<AddAuthorRequest, AddAuthorResponse>;
}
