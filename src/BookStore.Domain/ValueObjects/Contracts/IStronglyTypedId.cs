namespace BookStore.Domain.ValueObjects.Contracts
{
    public interface IStronglyTypedId<out TType> where TType : struct
    {
        TType Value { get; }
        bool HasValue { get; }
        string ToString();
    }
}
