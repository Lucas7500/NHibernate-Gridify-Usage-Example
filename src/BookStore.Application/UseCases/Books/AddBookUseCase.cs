using BookStore.Application.DTOs.Books.Requests;
using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.Mappers;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts;
using BookStore.Domain.ValueObjects;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class AddBookUseCase(IUnitOfWork unitOfWork, IValidator<AddBookRequest> requestValidator) : IAddBookUseCase
    {
        public async Task<ErrorOr<AddBookResponse>> ExecuteAsync(AddBookRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return Error.Validation(description: validationResult.ToString());
                }

                AuthorId authorId = new(request.AuthorId);
                Author? author = await unitOfWork.AuthorsRepository.GetByIdAsync(authorId, cancellationToken);

                if (author is null)
                {
                    return Error.NotFound(description: "Author not found.");
                }

                Book newBook = new(request.Title, request.Price, author);

                await unitOfWork.BooksRepository.AddAsync(newBook, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                return new AddBookResponse(newBook.ToBookWithAuthorResponse());
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                return Error.Failure(description: $"An error occurred while adding the book: {ex.Message}");
            }
        }
    }
}
