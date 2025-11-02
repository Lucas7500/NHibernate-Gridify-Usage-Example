using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Persistence.Contracts.Authors
{
    public interface IQueryableAuthorsRepository : IQueryableRepository<Author, AuthorId, Guid>;
}
