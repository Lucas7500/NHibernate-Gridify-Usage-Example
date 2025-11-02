namespace BookStore.Domain.ValueObjects.Contracts
{
    public interface IStronglyTypedId<TType> where TType : struct
    {
        TType Value { get; }
        bool HasValue { get; }
        string ToString();
    }
}
