namespace BookStore.Domain.Exceptions
{
    public sealed class InvalidIntegerIdException(string value) : Exception($"The provided value is not a valid integer id: {value}")
    {
        public static void ThrowIfNegativeOrZero(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIntegerIdException(id.ToString());
            }
        }
        
        public static void ThrowIfNegativeOrZero(string id)
        {
            if (!int.TryParse(id, out int parsedId) || parsedId <= 0)
            {
                throw new InvalidIntegerIdException(id);
            }
        }
    }
}
