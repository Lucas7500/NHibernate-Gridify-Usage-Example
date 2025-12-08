using BookStore.Application.DTOs.Authors.Requests;
using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.Mappers;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.ValueObjects;
using ErrorOr;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class AddBookUseCase(
        IBooksRepository booksRepository,
        IAuthorsRepository authorsRepository) : IAddBookUseCase
    {
        public async Task<ErrorOr<AddBookResponse>> ExecuteAsync(AddBookRequest request, CancellationToken cancellationToken = default)
        {
            if (!ValidateRequest(request))
            {
                return Error.Validation(description: "Invalid request data.");
            }

            AuthorId authorId = new(request.AuthorId);
            Author? author = await authorsRepository.GetByIdAsync(authorId, cancellationToken);
            
            if (author is null)
            {
                return Error.NotFound(description: "Author not found.");
            }

            Book newBook = new(request.Title, author, request.Price);
            await booksRepository.AddAsync(newBook, cancellationToken);

            return new AddBookResponse(newBook.ToBookWithAuthorResponse());
        }

        private static bool ValidateRequest(AddBookRequest request)
        {
            return true;
        }
    }
}
