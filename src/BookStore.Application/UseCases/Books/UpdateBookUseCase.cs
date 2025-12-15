using BookStore.Application.DTOs.Books.Requests;
using BookStore.Application.DTOs.Books.Responses;
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
    internal sealed class UpdateBookUseCase(
        IUnitOfWork unitOfWork,
        IValidator<UpdateBookRequest> requestValidator) : IUpdateBookUseCase
    {
        public async Task<ErrorOr<UpdateBookResponse>> ExecuteAsync(UpdateBookRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return Error.Validation(description: validationResult.ToString());
                }

                BookId bookId = new(request.BookId);
                Book? book = await unitOfWork.BooksRepository.GetByIdAsync(bookId, cancellationToken);

                if (book is null)
                {
                    return Error.NotFound(description: $"Book with Id '{request.BookId}' was not found.");
                }

                UpdateBookProperties(book, request);

                if (request.NewAuthorId.HasValue)
                {
                    AuthorId authorId = new(request.NewAuthorId.Value);
                    
                    Author? newAuthor = await unitOfWork.AuthorsRepository.GetByIdAsync(authorId, cancellationToken);

                    if (newAuthor is null)
                        return Error.NotFound(description: $"Author with Id {request.NewAuthorId} is not registered.");

                    book.ChangeAuthor(newAuthor);
                }

                await unitOfWork.CommitAsync(cancellationToken);

                return new UpdateBookResponse($"Book with Id '{request.BookId}' has been successfully updated.");
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                return Error.Failure(description: $"An error occurred while updating the book: {ex.Message}");
            }
        }

        private static void UpdateBookProperties(Book book, UpdateBookRequest request)
        {
            if (request.NewTitle is not null)
            {
                book.ChangeTitle(request.NewTitle);
            }

            if (request.NewIsAvailable.HasValue)
            {
                if (request.NewIsAvailable.Value)
                    book.MarkAsAvailable();

                if (!request.NewIsAvailable.Value)
                    book.MarkAsUnavailable();
            }
            
            if (request.NewPrice.HasValue)
            {
                book.ChangePrice(request.NewPrice.Value);
            }
        }
    }
}
