namespace BookStore.Domain.Validation
{
    public interface IBusinessRule
    {
        string Description { get; }
        bool IsBroken();
    }
}
