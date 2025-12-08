using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Persistence.Contracts.Authors
{
    public interface IAuthorsRepository : IRepository<Author, AuthorId, Guid>;
}
