using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Models.Base
{
    public abstract class Entity<TKey> : Entity where TKey : struct
    {
        public virtual TKey Id { get; protected set; }
    }

    public abstract class Entity
    {
        protected static void CheckRule(bool rule, string message)
            => BusinessRuleValidationException.ThrowIf(!rule, message);
    }
}
