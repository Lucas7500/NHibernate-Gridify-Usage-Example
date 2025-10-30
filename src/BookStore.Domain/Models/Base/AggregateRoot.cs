using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Models.Base
{
    public abstract class AggregateRoot<TKey> : Entity<TKey> where TKey : struct;
}
