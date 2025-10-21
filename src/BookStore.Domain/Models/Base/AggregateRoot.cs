using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Models.Base
{
    public abstract class AggregateRoot<TKey> where TKey : struct
    {
        public virtual TKey Id { get; protected set; }

        protected void CheckRule(bool rule, string message)
            => BusinessRuleValidationException.ThrowIf(rule, message);
    }
}
