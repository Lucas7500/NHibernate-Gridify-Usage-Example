namespace BookStore.Domain.Exceptions
{
    internal class InvalidIntegerIdException(string value) : Exception($"The provided value is not a valid integer id: {value}")
    {
        public static void ThrowIfInvalidIntegerId(string id)
        {
            if (!int.TryParse(id, out var parsedId))
            {
                throw new InvalidIntegerIdException(id);
            }
            
            ThrowIfEmpty(parsedId);
        }

        public static void ThrowIfEmpty(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIntegerIdException(id.ToString());
            }
        }
    }
}
