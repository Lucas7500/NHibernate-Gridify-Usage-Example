using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.ValueObjects;
using NHibernate;

namespace BookStore.Infra.Repositories.Authors
{
    internal sealed class AuthorsRepository(ISession session) : RepositoryBase<Author, AuthorId, Guid>(session), IAuthorsRepository
    {
    }
}
