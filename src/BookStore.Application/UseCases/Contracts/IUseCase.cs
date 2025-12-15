using ErrorOr;

namespace BookStore.Application.UseCases.Contracts
{
    public interface IUseCase<in TRequest, TResponse>
    {
        Task<ErrorOr<TResponse>> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
