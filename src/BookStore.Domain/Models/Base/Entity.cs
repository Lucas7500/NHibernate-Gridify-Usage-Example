using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.Base.BusinessRules;
using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Models.Base
{
    public abstract class Entity<TKey, TKeyValue> : Entity
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
    {
        private TKeyValue _keyValue;

        public virtual TKey Id { get; protected set; }

        /// <summary>
        /// Necessary for ORM tools that do not support strongly typed IDs.
        /// </summary>
        public virtual TKeyValue IdValue
        {
            get
            {
                return _keyValue;
            }
            set
            {
                _keyValue = value;
             
                if (!Id.HasValue)
                {
                    SetId(value);
                }
            }
        }

        protected Entity(TKey id)
        {
            CheckRule(new ProvidedIdCantBeEmpty<TKey, TKeyValue>(id));

            Id = id;
            IdValue = id.Value;
        }

        protected Entity() { }

        protected abstract void SetId(TKeyValue value);
    }

    public abstract class Entity
    {
        protected static void CheckRule(IBusinessRule rule)
            => BusinessRuleValidationException.ThrowIf(rule.IsBroken(), rule.Description);
    }
}
