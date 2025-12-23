using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Models.Base.BusinessRules
{
    internal class ProvidedIdCantBeEmpty<TKey, TKeyValue>
        : IBusinessRule
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
    {
        private readonly TKey _id;

        public ProvidedIdCantBeEmpty(TKey id)
        {
            _id = id;
        }

        public string Description => "Provided Id Can't be Empty";

        public bool IsBroken() => _id is not { HasValue: true };
    }
}
