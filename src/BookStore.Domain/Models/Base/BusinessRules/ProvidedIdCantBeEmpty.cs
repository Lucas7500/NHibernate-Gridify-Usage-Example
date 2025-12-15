using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Models.Base.BusinessRules
{
    public class ProvidedIdCantBeEmpty<TKey, TKeyValue>
        : IBusinessRule
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
    {
        public ProvidedIdCantBeEmpty(TKey id)
        {
            Id = id;
        }

        private TKey Id { get; }

        public string Description => "Provided Id Can't be Empty";

        public bool IsBroken() => Id is { HasValue: false };
    }
}
