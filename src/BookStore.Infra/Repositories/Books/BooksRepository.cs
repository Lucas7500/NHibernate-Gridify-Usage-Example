using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.ValueObjects;
using NHibernate;

namespace BookStore.Infra.Repositories.Books
{
    internal sealed class BooksRepository(ISession session) : RepositoryBase<Book, BookId, int>(session), IBooksRepository;
}
