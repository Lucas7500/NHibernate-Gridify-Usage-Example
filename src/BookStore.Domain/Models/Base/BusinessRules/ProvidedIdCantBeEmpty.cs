using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Models.Base.BusinessRules
{
    internal sealed class ProvidedIdCantBeEmpty<TKey, TKeyValue>
        : IBusinessRule
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
    {
        private readonly TKey _id;

        public ProvidedIdCantBeEmpty(TKey id)
        {
            _id = id;
        }

        public string Description => "businessrule.provided-id-cant-be-empty";

        public bool IsBroken() => _id is not { HasValue: true };
    }
}
