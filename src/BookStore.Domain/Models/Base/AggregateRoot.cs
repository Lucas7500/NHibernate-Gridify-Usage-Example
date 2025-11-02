using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Models.Base
{
    public abstract class AggregateRoot<TKey, TKeyValue> : Entity<TKey, TKeyValue> 
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
    {
        protected AggregateRoot(TKey id) : base(id)
        {
        }

        protected AggregateRoot() { }
    }
}
