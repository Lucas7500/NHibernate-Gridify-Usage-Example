using BookStore.Domain.Persistence.Contracts;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Infra.Repositories.Authors;
using BookStore.Infra.Repositories.Books;
using NHibernate;

namespace BookStore.Infra.NHibernate
{
    internal sealed class NHibernateUnitOfWork(NHibernateContext context) : IUnitOfWork, IDisposable
    {
        private readonly ITransaction _transaction = context.Session.BeginTransaction();

        private readonly Lazy<IBooksRepository> _booksRepository = new(() => new BooksRepository(context.Session));
        private readonly Lazy<IAuthorsRepository> _authorsRepository = new(() => new AuthorsRepository(context.Session));

        public IBooksRepository BooksRepository => _booksRepository.Value;
        public IAuthorsRepository AuthorsRepository => _authorsRepository.Value;

        public async Task CommitAsync(CancellationToken ct = default)
        {
            await _transaction.CommitAsync(ct);
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            await _transaction.RollbackAsync(ct);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
